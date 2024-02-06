using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Models
{
    public record CommentToReturnDto
    {
        public int Id { get; init; }
        public int SuggestionId { get; init; }
        public string Content { get; init; } = string.Empty;

        public User? User { get; init; }
    }
}