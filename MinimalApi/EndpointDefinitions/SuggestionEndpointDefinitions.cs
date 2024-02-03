using Application.Common;
using Application.Common.Models;
using Application.Suggestions.Commands;
using Application.Suggestions.Queries;
using MediatR;
using MinimalApi.Abstractions;

namespace MinimalApi.EndpointDefinitions
{
    public class SuggestionEndpointDefinitions : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var suggestions = app.MapGroup("/api/suggestions");

            suggestions.MapGet("/", GetSuggestions);
            suggestions.MapPost("/", CreateSuggestion);
            suggestions.MapGet("/{id}", GetSuggestionById)
            .WithName("GetSuggestionById");
        }

        private async Task<IResult> GetSuggestions(IMediator mediator)
        {
            var result = await mediator.Send(new GetSuggestions());
            return TypedResults.Ok(result);
        }

        private async Task<IResult> CreateSuggestion(IMediator mediator, SuggestionForCreationDto suggestion)
        {
            var command = new CreateSuggestion(suggestion);
            var result = await mediator.Send(command);
            return Results.CreatedAtRoute("GetSuggestionById", new { result.Id }, result);
        }

        private async Task<IResult> GetSuggestionById(IMediator mediator, int id)
        {
            var result = await mediator.Send(new GetSuggestionById(id));
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);
        }
    }
}