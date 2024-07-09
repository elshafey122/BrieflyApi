namespace Briefly.Core.Features.Article.Commands.Model
{
    public class AddLikeArticleCommand:IRequest<Response<string>>
    {
        public int ArticleId { get; set; }
    }
}
