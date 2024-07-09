using System.ComponentModel.DataAnnotations.Schema;

namespace Briefly.Data.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Link { get; set; }
        public string? Image { get; set; }
        public string? Category { get; set; }
        public string? Summarized { get; set; }
        public DateTime CreatedAt { get; set; }

        // new 
        public int Likes { get; set; }
        public int Views { get; set; }
        public int CommentsNumber { get; set; }
        public int ClusterLabel { get; set; }

        public int RSSId { get; set; }
        public RSS RSS { get; set; }


        public IEnumerable<Comment>? CommentArticles { get; set; }

        [NotMapped]
        public IEnumerable<Article>? Clusters { get; set; }

    }
}

