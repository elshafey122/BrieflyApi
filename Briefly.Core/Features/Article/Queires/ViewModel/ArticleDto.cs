namespace Briefly.Core.Features.Article.Queires.ViewModel
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public int? Views { get; set; }
        public int? Likes { get; set; }
        public int? CommentsNumber { get; set; }
        public string? Image { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
