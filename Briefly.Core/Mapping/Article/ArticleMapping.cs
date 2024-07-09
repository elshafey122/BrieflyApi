namespace Briefly.Core.Mapping.Articles
{
    public partial class ArticleMapping : Profile
    {
        public ArticleMapping()
        {
            GetRssArticleByUrlMapper();
            GetAllRssArticlesMapper();
        }
    }
}
