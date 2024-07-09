namespace Briefly.Infrastructure.IRepositoties
{
    public interface IRssRepository:IGenericRepository<RSS>
    {
        Task<RSS> AddRssAsync(RSS rss);
        Task<List<RSS>> GetRssSubscribtions(string userId);
        Task<RSS> CheckRssExist(string Rssurl);
    }
}
