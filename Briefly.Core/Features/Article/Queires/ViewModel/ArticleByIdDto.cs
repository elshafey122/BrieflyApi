namespace Briefly.Core.Features.Article.Queires.ViewModel
{
    public class ArticleByIdDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Link { get; set; }

        //
        public String? Summarized { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
        public List<ArticleDto>? Clusters { get; set; }
        //

        public int? Views { get; set; }
        public int? Likes { get; set; }
        public int? CommentsNumber { get; set; }

    }
}
