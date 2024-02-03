using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Suggestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = String.Empty;

        public int Upvotes { get; set; }

        [MaxLength(50)]
        public string Category { get; set; } = String.Empty;


        [MaxLength(50)]
        public string Status { get; set; } = String.Empty;


        [MaxLength(500)]
        public string Description { get; set; } = String.Empty;

        public IEnumerable<SuggestionComment> Comments { get; set; } = new List<SuggestionComment>();

        public Suggestion() { }
        public Suggestion(string title, string category, string status, string description)
        {
            Title = title;
            Category = category;
            Status = status;
            Description = description;
        }
    }
}
