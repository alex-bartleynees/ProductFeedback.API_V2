using Application.Abstractions;
using Application.Common;
using Application.Common.Models;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.Suggestions.Commands
{
    public record UpdateSuggestion(SuggestionForUpdateDto suggestion) : IRequest<Result<Suggestion>>;
    public class UpdateSuggestionHandler : IRequestHandler<UpdateSuggestion, Result<Suggestion>>
    {
        private readonly ISuggestionsRepository _suggestionsRepository;
        public UpdateSuggestionHandler(ISuggestionsRepository suggestionsRepository)
        {
            _suggestionsRepository = suggestionsRepository ??
                throw new ArgumentNullException(nameof(suggestionsRepository));
        }
        public async Task<Result<Suggestion>> Handle(UpdateSuggestion request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request);

            var suggestion = new Suggestion()
            {
                Title = request.suggestion.Title,
                Upvotes = request.suggestion.Upvotes,
                Status = request.suggestion.Status,
                Category = request.suggestion.Category,
                Description = request.suggestion.Description
            };

            return await _suggestionsRepository.UpdateSuggestion(request.suggestion.Id, suggestion);
        }
    }
}