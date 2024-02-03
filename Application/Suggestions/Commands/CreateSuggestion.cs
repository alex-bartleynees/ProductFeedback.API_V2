using Application.Abstractions;
using Application.Common.Models;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.Suggestions.Commands
{
    public record CreateSuggestion(SuggestionForCreationDto suggestion) : IRequest<Suggestion>;
    public class CreateSuggestionHandler : IRequestHandler<CreateSuggestion, Suggestion>
    {
        private readonly ISuggestionsRepository _suggestionsRepository;
        public CreateSuggestionHandler(ISuggestionsRepository suggestionsRepository)
        {
            _suggestionsRepository = suggestionsRepository ??
                throw new ArgumentNullException(nameof(suggestionsRepository));
        }
        public async Task<Suggestion> Handle(CreateSuggestion request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request);

            var suggestionEntity = new Suggestion(request.suggestion.Title, request.suggestion.Category, "", request.suggestion.Description);

            var result = await _suggestionsRepository.CreateSuggestion(suggestionEntity);
            var id = result.FirstOrDefault();
            suggestionEntity.Id = id;

            return suggestionEntity;
        }
    }
}