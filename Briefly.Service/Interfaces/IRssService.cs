
namespace Briefly.Service.Interfaces
{
    public interface IRssService
    {
        Task<string> CreateUserRss(string userId, string url);
        Task<string> CreateRss(string rssUrl);
        Task<string> RssUserSubscribe(string userId, int rssId);
        Task<string> RssUnUserSubscribe(string userId, int rssId);
        Task<List<RSS>> GetAllSubscribedRss(string userId); 
        Task<List<RSS>> GetAllRss(string search);
        Task<RSS> GetRssById(int rssId);
        Task UpdateRss(RSS rss);
        Task<string> DeleteRss(RSS rss);
    }
}
