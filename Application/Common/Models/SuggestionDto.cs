

using Domain.Entities;

namespace Application.Common.Models
{
    public record SuggestionDto
    {
        public SuggestionDto(Suggestion s, int commentCount)
        {
            Id = s.Id;
            Title = s.Title;
            Upvotes = s.Upvotes;
            Category = s.Category;
            Status = s.Status;
            Description = s.Description;
            CommentCount = commentCount;
        }
        public int Id { get; init; }
        public string Title { get; init; }
        public int Upvotes { get; init; }
        public string Category { get; init; }

        public string Status { get; init; }

        public string Description { get; init; }

        public int CommentCount { get; init; }

    }
}