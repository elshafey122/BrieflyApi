using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Briefly.Data.Entities
{
    [Index(nameof(ArticleId))]
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime PostedDate { get; set; } = DateTime.Now;
        public int Likes { get; set; }

        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [JsonIgnore]
        public List<Comment>? Replies { get; set; } 
    }
}
