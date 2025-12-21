using Application.Abstractions;
using Application.Common;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;

namespace Application.Suggestions.Commands
{
    public record DeleteSuggestion(int suggestionId) : IRequest<Result<int>>;
    public class DeleteSuggestionHandler : IRequestHandler<DeleteSuggestion, Result<int>>
    {
        private readonly ISuggestionsRepository _suggestionsRepository;
        private readonly HybridCache _cache;

        public DeleteSuggestionHandler(ISuggestionsRepository suggestionsRepository, HybridCache cache)
        {
            _suggestionsRepository = suggestionsRepository ??
                throw new ArgumentNullException(nameof(suggestionsRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        public async Task<Result<int>> Handle(DeleteSuggestion request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request);

            var result = await _suggestionsRepository.DeleteSuggestion(request.suggestionId);
            if (result.IsSuccess)
            {
                await _cache.RemoveAsync($"suggestion_{request.suggestionId}", cancellationToken);
            }
            return result;
        }
    }
}