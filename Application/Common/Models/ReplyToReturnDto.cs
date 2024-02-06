using Domain.Entities;

namespace Application.Common.Models
{
    public record ReplyToReturnDto
    {
        public int Id { get; init; }

        public int SuggestionId { get; init; }
        public int SuggestionCommentId { get; init; }
        public string Content { get; init; } = string.Empty;
        public string ReplyingTo { get; init; } = string.Empty;

        public User? User { get; init; }
    }
}