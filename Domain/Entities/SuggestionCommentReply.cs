using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class SuggestionCommentReply
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        public string ReplyingTo { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int UserId { get; set; }

        [ForeignKey("SuggestionCommentId")]
        public int? SuggestionCommentId { get; set; }

        public SuggestionCommentReply(string content, string replyingTo)
        {
            Content = content;
            ReplyingTo = replyingTo;
        }
    }
}