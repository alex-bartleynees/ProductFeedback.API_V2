namespace Application.Common.Models
{
    public record CommentForCreationDto
    {
        public int SuggestionId { get; init; }
        public string Content { get; init; } = string.Empty;

        public Guid UserId { get; init; }
    }
}