using Application.Common;
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

            users.MapGet("{userId}", GetUserById);
        }

        private async Task<Results<Ok<User>, NotFound<Error>>> GetUserById(IMediator mediator, int userId)
        {
            var command = new GetUserById(userId);
            var result = await mediator.Send(command);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);
        }
    }
}