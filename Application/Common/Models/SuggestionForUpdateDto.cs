using System.ComponentModel.DataAnnotations;


namespace Application.Common.Models
{
    public record SuggestionForUpdateDto
    {
        public int Id { get; init; }

        [Required(ErrorMessage = "You must provide a title.")]
        [MaxLength(100)]
        public string Title { get; init; }

        public int Upvotes { get; init; }

        public string Category { get; init; }

        public string Status { get; init; }

        public string Description { get; init; }

        public SuggestionForUpdateDto(string title, int upvotes, string category, string status, string description)
        {
            Title = title;
            Upvotes = upvotes;
            Category = category;
            Status = status;
            Description = description;
        }
    }
}