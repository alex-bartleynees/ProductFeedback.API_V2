using Application.Common;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Abstractions
{
    public interface ISuggestionsRepository
    {
        Task<IEnumerable<SuggestionDto>> GetSuggestions();

        Task<Result<Suggestion>> GetSuggestionById(int suggestionId);

        Task<IEnumerable<int>> CreateSuggestion(Suggestion suggestion);

        Task<IEnumerable<int>> AddCommentToSuggestion(SuggestionComment comment);

        Task<IEnumerable<int>> AddReplyToComment(SuggestionCommentReply reply);

        Task<Result<User>> GetUser(int userId);

        Task<Result<int>> DeleteSuggestion(int suggestionId);

        Task<Result<Suggestion>> UpdateSuggestion(int suggestionId, Suggestion suggestion);
    }
}
