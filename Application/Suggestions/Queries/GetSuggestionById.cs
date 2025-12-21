using Application.Abstractions;
using Application.Common;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;

namespace Application.Suggestions.Queries
{
    public record GetSuggestionById(int suggestionId) : IRequest<Result<Suggestion>>;

    public class GetSuggestionByIdHandler : IRequestHandler<GetSuggestionById, Result<Suggestion>>
    {

        private readonly ISuggestionsRepository _suggestionsRepository;
        private readonly HybridCache _cache;

        public GetSuggestionByIdHandler(ISuggestionsRepository suggestionsRepository, HybridCache cache)
        {
            _suggestionsRepository = suggestionsRepository ??
                throw new ArgumentNullException(nameof(suggestionsRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        public async Task<Result<Suggestion>> Handle(GetSuggestionById request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request);

            // Cache only the Suggestion entity, not the Result wrapper
            var suggestion = await _cache.GetOrCreateAsync<Suggestion?>(
                $"suggestion_{request.suggestionId}",
                async cancel =>
                {
                    var result = await _suggestionsRepository.GetSuggestionById(request.suggestionId);
                    return result.IsSuccess ? result.Value : null;
                },
                cancellationToken: cancellationToken
            );

            if (suggestion == null)
            {
                return Result<Suggestion>.Failure(new Error(404, "Suggestion.NotFound", "Suggestion not found"));
            }

            return Result<Suggestion>.Success(suggestion);
        }
    }
}