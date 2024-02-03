using System.ComponentModel.DataAnnotations;

namespace Application.Common.Models
{
    public record SuggestionForCreationDto
    {
        [Required(ErrorMessage = "You must provide a title.")]
        [MaxLength(100)]
        public string Title { get; init; }

        public string Category { get; init; }

        public string Description { get; init; }

        public SuggestionForCreationDto(string title, string category, string description)
        {
            Title = title;
            Category = category;
            Description = description;
        }
    }
}