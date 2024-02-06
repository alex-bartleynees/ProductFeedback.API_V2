using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Users.Queries;
using MediatR;
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

        private async Task<IResult> GetUserById(IMediator mediator, int userId)
        {
            var command = new GetUserById(userId);
            var result = await mediator.Send(command);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);
        }
    }
}