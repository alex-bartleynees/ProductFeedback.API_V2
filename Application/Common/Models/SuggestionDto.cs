

using Domain.Entities;

namespace Application.Common.Models
{
    public record SuggestionDto
    {
        public SuggestionDto(Suggestion s)
        {
            Id = s.Id;
            Title = s.Title;
            Upvotes = s.Upvotes;
            Category = s.Category;
            Status = s.Status;
            Description = s.Description;
        }
        public int Id { get; set; }
        public string Title { get; init; }
        public int Upvotes { get; set; }
        public string Category { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

    }
}