using Application.Abstractions;
using Application.Common;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Comments.Commands
{
    public record CreateCommentReply(ReplyForCreationDto reply) : IRequest<Result<ReplyToReturnDto>>;
    public class CreateCommentReplyHandler : IRequestHandler<CreateCommentReply, Result<ReplyToReturnDto>>
    {
        private readonly ISuggestionsRepository _suggestionsRepository;
        public CreateCommentReplyHandler(ISuggestionsRepository suggestionsRepository)
        {
            _suggestionsRepository = suggestionsRepository ??
                throw new ArgumentNullException(nameof(suggestionsRepository));
        }
        public async Task<Result<ReplyToReturnDto>> Handle(CreateCommentReply request, CancellationToken cancellationToken)
        {
            var replyEntity = new SuggestionCommentReply(request.reply.Content, request.reply.ReplyingTo)
            {
                UserId = request.reply.UserId,
                SuggestionCommentId = request.reply.SuggestionCommentId
            };

            var result = await _suggestionsRepository.AddReplyToComment(replyEntity);
            var id = result.FirstOrDefault();
            replyEntity.Id = id;

            var userResult = await _suggestionsRepository.GetUser(request.reply.UserId);

            if (userResult.IsFailure)
            {
                return Result<ReplyToReturnDto>.Failure(new Error(404, "Not Found", $"User with id ${request.reply.UserId} was not found"));
            }

            var user = userResult.Value;
            var replyToReturn = new ReplyToReturnDto()
            {
                Id = replyEntity.Id,
                SuggestionId = request.reply.SuggestionId,
                SuggestionCommentId = (int)replyEntity.SuggestionCommentId,
                Content = replyEntity.Content,
                ReplyingTo = request.reply.ReplyingTo,
                User = user,
            };

            return Result<ReplyToReturnDto>.Success(replyToReturn);
        }
    }
}