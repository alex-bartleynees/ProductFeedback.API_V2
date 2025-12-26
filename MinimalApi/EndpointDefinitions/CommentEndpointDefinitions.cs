using Application.Comments.Commands;
using Application.Common;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MinimalApi.Abstractions;

namespace MinimalApi.EndpointDefinitions
{
    public class CommentEndpointDefinitions : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var comments = app.MapGroup("/api/comment");

            comments.MapPost("/", AddCommentToSuggestion).RequireAuthorization();
            comments.MapPost("reply", AddReplyToComment).RequireAuthorization();
        }

        private async Task<Results<Ok<CommentToReturnDto>, NotFound<Error>>> AddCommentToSuggestion(IMediator mediator, CommentForCreationDto comment)
        {
            var command = new CreateComment(comment);
            var result = await mediator.Send(command);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);
        }

        private async Task<Results<Ok<ReplyToReturnDto>, NotFound<Error>>> AddReplyToComment(IMediator mediator, ReplyForCreationDto reply)
        {
            var command = new CreateCommentReply(reply);
            var result = await mediator.Send(command);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);

        }
    }
}