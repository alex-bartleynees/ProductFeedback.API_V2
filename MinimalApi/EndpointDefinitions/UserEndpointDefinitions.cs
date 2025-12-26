using Application.Common;
using Application.Common.Models;
using Application.Users.Commands;
using Application.Users.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MinimalApi.Abstractions;

namespace MinimalApi.EndpointDefinitions
{
    public class UserEndpointDefinitions : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var users = app.MapGroup("api/users");

            users.MapGet("{userId}", GetUserById).RequireAuthorization();
            users.MapPost("", CreateUser);
        }

        private async Task<Results<Ok<User>, NotFound<Error>>> GetUserById(IMediator mediator, Guid userId)
        {
            var command = new GetUserById(userId);
            var result = await mediator.Send(command);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);
        }

        private async Task<Results<Created<User>, Conflict<Error>, BadRequest<Error>>> CreateUser(
            IMediator mediator,
            UserForCreationDto user)
        {
            var command = new CreateUser(user);
            var result = await mediator.Send(command);

            if (result.IsFailure)
            {
                if (result.Error.Status == 409)
                {
                    return TypedResults.Conflict(result.Error);
                }
                return TypedResults.BadRequest(result.Error);
            }

            return TypedResults.Created($"/api/users/{result.Value!.Id}", result.Value);
        }
    }
}