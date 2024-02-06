using Application.Comments.Commands;
using Application.Common.Models;
using MediatR;
using MinimalApi.Abstractions;

namespace MinimalApi.EndpointDefinitions
{
    public class CommentEndpointDefinitions : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var comments = app.MapGroup("/api/comment");

            comments.MapPost("/", AddCommentToSuggestion);
            comments.MapPost("reply", AddReplyToComment);
        }

        private async Task<IResult> AddCommentToSuggestion(IMediator mediator, CommentForCreationDto comment)
        {
            var command = new CreateComment(comment);
            var result = await mediator.Send(command);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);
        }

        private async Task<IResult> AddReplyToComment(IMediator mediator, ReplyForCreationDto reply)
        {
            var command = new CreateCommentReply(reply);
            var result = await mediator.Send(command);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);

        }
    }
}