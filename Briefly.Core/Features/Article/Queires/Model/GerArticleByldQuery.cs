namespace Briefly.Core.Features.Article.Queires.Model
{
    public class GerArticleByldQuery: IRequest<Response<ArticleByIdDto>>
    {
        public int ArticleId{ get; set; }
    }
}
