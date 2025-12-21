using Application.Abstractions;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Suggestions.Queries
{
    public record GetSuggestions : IRequest<IEnumerable<SuggestionDto>>;

    public class GetSuggestionsHandler : IRequestHandler<GetSuggestions, IEnumerable<SuggestionDto>>
    {
        private readonly ISuggestionsRepository _suggestionsRepository;
        private readonly ILogger<GetSuggestionsHandler> _logger;

        public GetSuggestionsHandler(ISuggestionsRepository suggestionsRepository, ILogger<GetSuggestionsHandler> logger)
        {
            _suggestionsRepository = suggestionsRepository ??
                throw new ArgumentNullException(nameof(suggestionsRepository));
            _logger = logger;
        }

        public async Task<IEnumerable<SuggestionDto>> Handle(GetSuggestions request, CancellationToken cancellationToken)
        {
            return await _suggestionsRepository.GetSuggestions();
        }
    }
}
