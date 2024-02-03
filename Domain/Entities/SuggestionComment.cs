using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class SuggestionComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int UserId { get; set; }

        [ForeignKey("SuggestionId")]
        public int SuggestionId { get; set; }

        public ICollection<SuggestionCommentReply> Replies { get; set; } = new List<SuggestionCommentReply>();

        public SuggestionComment(string content)
        {
            Content = content;
        }
    }
}