using Application.Abstractions;
using Application.Common;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.Suggestions.Queries
{
    public record GetSuggestionById(int suggestionId) : IRequest<Result<Suggestion>>;

    public class GetSuggestionByIdHandler : IRequestHandler<GetSuggestionById, Result<Suggestion>>
    {

        private readonly ISuggestionsRepository _suggestionsRepository;

        public GetSuggestionByIdHandler(ISuggestionsRepository suggestionsRepository)
        {
            _suggestionsRepository = suggestionsRepository ??
                throw new ArgumentNullException(nameof(suggestionsRepository));
        }
        public async Task<Result<Suggestion>> Handle(GetSuggestionById request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request);

            return await _suggestionsRepository.GetSuggestionById(request.suggestionId);
        }
    }
}