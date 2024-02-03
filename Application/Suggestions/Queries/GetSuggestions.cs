using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Suggestions.Queries
{
    public record GetSuggestions : IRequest<IEnumerable<Suggestion>>;

    public class GetSuggestionsHandler : IRequestHandler<GetSuggestions, IEnumerable<Suggestion>>
    {
        private readonly ISuggestionsRepository _suggestionsRepository;

        public GetSuggestionsHandler(ISuggestionsRepository suggestionsRepository)
        {
            _suggestionsRepository = suggestionsRepository ??
                throw new ArgumentNullException(nameof(suggestionsRepository));
        }

        public async Task<IEnumerable<Suggestion>> Handle(GetSuggestions request, CancellationToken cancellationToken)
        {
            return await _suggestionsRepository.GetSuggestions();
        }
    }
}