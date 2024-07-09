namespace Briefly.Infrastructure.IRepositoties
{
    public interface IRssSubscribRepository:IGenericRepository<RssSubscription>
    {
        Task<bool> CheckSubscribtionRss(int Userid, int Rssid);
    }
}
