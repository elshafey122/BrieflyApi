namespace Briefly.Infrastructure.IRepositoties
{
    public interface IArticleRepository : IGenericRepository<Article>
    {
        Task <List<ICollection<Article>>> GetAllArticles(int RssId);

        

        //Task<string> SummarizeAsync(string ArticlUsrl);
    }
}
