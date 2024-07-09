namespace Briefly.Core.Features.Article.Commands.Model
{
    public class SaveUserArticleCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public int ArticleId { get; set; }
    }
}
