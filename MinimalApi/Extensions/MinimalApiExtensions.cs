using Application.Abstractions;
using Application.Common.Models;
using Application.Common.Validators;
using Application.Suggestions.Commands;
using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var cs = builder.Configuration.GetConnectionString("SuggestionDBConnectionString");
            builder.Services.AddDbContext<SuggestionContext>(options => options.UseSqlServer(cs));
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
                                      policy.WithOrigins("http://localhost:4200", "https://product-feedback-app-v2.netlify.app/")
                                          .AllowAnyHeader()
                                         .AllowAnyMethod();
                                  });
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddProblemDetails();
        }

        public static void RegisterAppConfig(this WebApplication app)
        {
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