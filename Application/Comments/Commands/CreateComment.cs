using Application.Abstractions;
using Application.Common;
using Application.Common.Models;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.Comments.Commands
{
    public record CreateComment(CommentForCreationDto comment) : IRequest<Result<CommentToReturnDto>>;
    public class CreateCommentHandler : IRequestHandler<CreateComment, Result<CommentToReturnDto>>
    {

        private readonly ISuggestionsRepository _suggestionsRepository;
        public CreateCommentHandler(ISuggestionsRepository suggestionsRepository)
        {
            _suggestionsRepository = suggestionsRepository ??
                throw new ArgumentNullException(nameof(suggestionsRepository));
        }

        public async Task<Result<CommentToReturnDto>> Handle(CreateComment request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request);

            var commentEntity = new SuggestionComment(request.comment.Content)
            {
                UserId = request.comment.UserId,
                SuggestionId = request.comment.SuggestionId
            };
            var result = await _suggestionsRepository.AddCommentToSuggestion(commentEntity);
            var id = result.FirstOrDefault();
            commentEntity.Id = id;

            var userResult = await _suggestionsRepository.GetUser(request.comment.UserId);

            if (userResult.IsFailure)
            {
                return Result<CommentToReturnDto>.Failure(new Error(404, "Not Found", $"User with id ${request.comment.UserId} was not found"));
            }

            var user = userResult.Value;

            var commentToReturn = new CommentToReturnDto()
            {
                Id = commentEntity.Id,
                SuggestionId = commentEntity.SuggestionId,
                Content = commentEntity.Content,
                User = user,
            };

            return Result<CommentToReturnDto>.Success(commentToReturn);
        }
    }
}