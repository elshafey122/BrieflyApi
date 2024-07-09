namespace Briefly.Core.Features.Article.Commands.Model
{
    public class DeletelikeArticleCommand : IRequest<Response<string>>
    {
        public int ArticleId { get; set; }
    }
}
