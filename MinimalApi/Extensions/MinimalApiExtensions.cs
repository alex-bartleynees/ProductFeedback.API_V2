using Amazon.S3;
using Application.Abstractions;
using Application.Common.Models;
using Application.Common.Validators;
using Application.Suggestions.Commands;
using DataAccess.Configuration;
using DataAccess.DbContexts;
using DataAccess.Repositories;
using DataAccess.Services;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MinimalApi.Abstractions;

using MinimalApi.Middleware;

using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using StackExchange.Redis;

namespace MinimalApi.Extensions
{
    public static class MinimalApiExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var cs = builder.Configuration.GetConnectionString("SuggestionDBConnectionString") ?? throw new ArgumentNullException(nameof(builder), "No connection string provided");
            builder.Services.AddDbContext<SuggestionContext>(options => options.UseNpgsql(cs, options => options.EnableRetryOnFailure()));

            // Add StackExchangeRedisCache service for Redis
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                var redisConfig = ConfigurationOptions.Parse(
                    builder.Configuration.GetConnectionString("RedisConnection") ?? "localhost:6379"
                );

                // Set password from configuration
                redisConfig.Password = builder.Configuration["Redis:Password"];

                // Optional: Configure TLS
                if (builder.Configuration.GetValue<bool>("Redis:UseTls"))
                {
                    redisConfig.Ssl = true;
                    redisConfig.SslHost = builder.Configuration["Redis:Host"];

                    // Allow self-signed certificates (common in Kubernetes internal Redis)
                    redisConfig.CertificateValidation += (sender, cert, chain, errors) => true;
                }

                // Retry configuration
                redisConfig.AbortOnConnectFail = false;
                redisConfig.ConnectRetry = 3;

                options.ConfigurationOptions = redisConfig;
                options.InstanceName = "SuggestionsCache:";
            });

            // Add HybridCache service
            builder.Services.AddHybridCache();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ISuggestionsRepository, SuggestionsRepository>();

            // Keycloak HTTP Client
            builder.Services.Configure<KeycloakSettings>(builder.Configuration.GetSection("Keycloak"));
            builder.Services.AddHttpClient<IKeycloakService, KeycloakService>("KeycloakClient", client =>
            {
                var keycloakBaseUrl = builder.Configuration["Keycloak:BaseUrl"]
                    ?? throw new ArgumentNullException("Keycloak:BaseUrl", "Keycloak BaseUrl must be configured");
                client.BaseAddress = new Uri(keycloakBaseUrl);
            });

            // Blob Storage (SeaweedFS with S3 API)
            builder.Services.Configure<BlobStorageSettings>(builder.Configuration.GetSection("BlobStorage"));
            builder.Services.AddSingleton<IAmazonS3>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<BlobStorageSettings>>().Value;
                var config = new AmazonS3Config
                {
                    ServiceURL = settings.ServiceUrl,
                    ForcePathStyle = settings.ForcePathStyle,
                    UseHttp = settings.ServiceUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                };

                // Use anonymous credentials if no access key configured (SeaweedFS without auth)
                if (string.IsNullOrEmpty(settings.AccessKey))
                {
                    return new AmazonS3Client(new Amazon.Runtime.AnonymousAWSCredentials(), config);
                }

                var credentials = new Amazon.Runtime.BasicAWSCredentials(settings.AccessKey, settings.SecretKey);
                return new AmazonS3Client(credentials, config);
            });
            builder.Services.AddScoped<IBlobStorageService, SeaweedFsBlobStorageService>();

            builder.Services.AddScoped<IValidator<SuggestionForCreationDto>, SuggestionForCreationDtoValidator>();
            builder.Services.AddScoped<IValidator<SuggestionForUpdateDto>, SuggestionForUpdateDtoValidator>();
            builder.Services.AddFluentValidationAutoValidation(configuration =>
            {
                configuration.OverrideDefaultResultFactoryWith<ValidationErrorFactory>();
            });
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200",
                                                         "https://product-feedback-app-v2.netlify.app",
                                                         "https://product-feedback-app.calmisland-859a0406.australiaeast.azurecontainerapps.io",
                                                         "https://suggestions-app-ssr.azurewebsites.net",
                                                         "https://product-feedback.alexbartleynees.com",
                                                         "https://product-feedback-ssr.netlify.app")
                                          .AllowAnyHeader()
                                         .AllowAnyMethod();
                                  });
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddProblemDetails();

            builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                    {
                        options.Authority = builder.Configuration["Jwt:Authority"]
                            ?? throw new ArgumentNullException("Jwt:Authority", "JWT Authority must be configured");
                        options.Audience = builder.Configuration["Jwt:Audience"]
                            ?? throw new ArgumentNullException("Jwt:Audience", "JWT Audience must be configured");
                    }
                );

            builder.Services.AddAuthorizationBuilder();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Suggestions API",
                    Description = "Suggestions API documentation",
                });
            });
        }

        public static void RegisterAppConfig(this WebApplication app)
        {

            var config = $"appsettings.{app.Environment}.json";
            IConfigurationBuilder configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true)
                .AddJsonFile(config, true)
                .AddEnvironmentVariables();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            app.UseCors(MyAllowSpecificOrigins);

            app.RegisterEndpointDefinitions();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            try
            {
                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<SuggestionContext>();
                    db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to migrate database", ex);
            }
        }

        public static void RegisterEndpointDefinitions(this WebApplication app)
        {
            var endpointDefinitions = typeof(Program).Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsAbstract && !t.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();

            foreach (var endpointDefinition in endpointDefinitions)
            {
                endpointDefinition.RegisterEndpoints(app);
            }
        }
    }
}
