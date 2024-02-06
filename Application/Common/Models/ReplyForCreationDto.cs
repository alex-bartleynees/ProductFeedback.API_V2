namespace Application.Common.Models
{
    public record ReplyForCreationDto
    {
        public int SuggestionId { get; init; }
        public int SuggestionCommentId { get; init; }
        public string Content { get; init; } = string.Empty;

        public string ReplyingTo { get; init; } = string.Empty;

        public int UserId { get; init; }
    }
}