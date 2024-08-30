using Application.Abstractions;
using Application.Common.Models;
using Application.Common.Validators;
using Application.Suggestions.Commands;
using DataAccess.DbContexts;
using DataAccess.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MinimalApi.Abstractions;
using MinimalApi.Middleware;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace MinimalApi.Extensions
{
    public static class MinimalApiExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var cs = builder.Configuration.GetConnectionString("SuggestionDBConnectionString") ?? throw new ArgumentNullException(nameof(builder), "No connection string provided");
            builder.Services.AddDbContext<SuggestionContext>(options => options.UseSqlServer(cs, options => options.MigrationsAssembly("MinimalApi")));
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ISuggestionsRepository, SuggestionsRepository>();
            builder.Services.AddScoped<IValidator<SuggestionForCreationDto>, SuggestionForCreationDtoValidator>();
            builder.Services.AddScoped<IValidator<SuggestionForUpdateDto>, SuggestionForUpdateDtoValidator>();
            builder.Services.AddFluentValidationAutoValidation(configuration =>
            {
                configuration.OverrideDefaultResultFactoryWith<ValidationErrorFactory>();
            });
            builder.Services.AddMediatR(typeof(CreateSuggestion));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200",
                                                         "https://product-feedback-app-v2.netlify.app",
                                                         "https://product-feedback-app.calmisland-859a0406.australiaeast.azurecontainerapps.io",
                                                         "https://suggestions-app-ssr.azurewebsites.net",
                                                         "https://product-feedback-ssr.netlify.app")
                                          .AllowAnyHeader()
                                         .AllowAnyMethod();
                                  });
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddProblemDetails();

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