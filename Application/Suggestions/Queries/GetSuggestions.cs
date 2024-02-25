using Application.Abstractions;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Suggestions.Queries
{
    public record GetSuggestions : IRequest<IEnumerable<SuggestionDto>>;

    public class GetSuggestionsHandler : IRequestHandler<GetSuggestions, IEnumerable<SuggestionDto>>
    {
        private readonly ISuggestionsRepository _suggestionsRepository;

        public GetSuggestionsHandler(ISuggestionsRepository suggestionsRepository)
        {
            _suggestionsRepository = suggestionsRepository ??
                throw new ArgumentNullException(nameof(suggestionsRepository));
        }

        public async Task<IEnumerable<SuggestionDto>> Handle(GetSuggestions request, CancellationToken cancellationToken)
        {
            return await _suggestionsRepository.GetSuggestions();
        }
    }
}