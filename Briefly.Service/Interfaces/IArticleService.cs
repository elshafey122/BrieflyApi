namespace Briefly.Service.Interfaces
{
    public interface IArticleService
    {
        Task<(string, Article)> GetRssArticlesById(int ArticleId);
        Task<(string, List<Article>)> GetAllRssArtricles(int RssId);
        Task<(string, List<Article>)> GetAllSaveUserArtricles(int userId);

        Task<string> SaveUserArticle(string userId, int articleId);
        Task<string> DeleteUserArticle(string userId, int articleId);

        Task<string> AddLikeArticle(int id);
        Task<string> DeleteLikeArticle(int id);
    }
}
