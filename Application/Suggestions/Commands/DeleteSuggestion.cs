using Application.Abstractions;
using Application.Common;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Suggestions.Commands
{
    public record DeleteSuggestion(int suggestionId) : IRequest<Result<int>>;
    public class DeleteSuggestionHandler : IRequestHandler<DeleteSuggestion, Result<int>>
    {
        private readonly ISuggestionsRepository _suggestionsRepository;
        public DeleteSuggestionHandler(ISuggestionsRepository suggestionsRepository)
        {
            _suggestionsRepository = suggestionsRepository ??
                throw new ArgumentNullException(nameof(suggestionsRepository));
        }
        public Task<Result<int>> Handle(DeleteSuggestion request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request);

            return _suggestionsRepository.DeleteSuggestion(request.suggestionId);
        }
    }
}