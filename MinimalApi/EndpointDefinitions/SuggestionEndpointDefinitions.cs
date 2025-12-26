using System.Diagnostics;
using Application.Common;
using Application.Common.Models;
using Application.Suggestions.Commands;
using Application.Suggestions.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MinimalApi.Abstractions;
using MinimalApi.Diagnostics;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace MinimalApi.EndpointDefinitions
{
    public class SuggestionEndpointDefinitions : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var suggestions = app.MapGroup("/api/suggestions").AddFluentValidationAutoValidation(); ;

            suggestions.MapGet("/", GetSuggestions);
            suggestions.MapPost("/", CreateSuggestion).RequireAuthorization();
            suggestions.MapGet("/{id}", GetSuggestionById)
            .WithName("GetSuggestionById");
            suggestions.MapPut("/{suggestionId}", UpdateSuggestion).RequireAuthorization();
            suggestions.MapDelete("/{suggestionId}", DeleteSuggestion).RequireAuthorization();
        }

        private async Task<Ok<IEnumerable<SuggestionDto>>> GetSuggestions(IMediator mediator)
        {
            var result = await mediator.Send(new GetSuggestions());
            return TypedResults.Ok(result);
        }

        private async Task<IResult> CreateSuggestion(IMediator mediator, SuggestionForCreationDto suggestion)
        {
            var command = new CreateSuggestion(suggestion);
            var result = await mediator.Send(command);
            ApplicationsDiagnostics.SuggestionsCreatedCounter.Add(1);
            return Results.CreatedAtRoute("GetSuggestionById", new { result.Id }, result);
        }

        private async Task<Results<Ok<Suggestion>, NotFound<Error>>> GetSuggestionById(IMediator mediator, int id)
        {
            var result = await mediator.Send(new GetSuggestionById(id));
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);
        }

        private async Task<Results<Ok<Suggestion>, NotFound<Error>>> UpdateSuggestion(IMediator mediator, int suggestionId, SuggestionForUpdateDto suggestion)
        {
            var command = new UpdateSuggestion(suggestion);
            var result = await mediator.Send(command);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);
        }

        private async Task<Results<Ok<int>, NotFound<Error>>> DeleteSuggestion(IMediator mediator, int suggestionId)
        {
            var command = new DeleteSuggestion(suggestionId);
            var result = await mediator.Send(command);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);
        }
    }
}
