using Microsoft.AspNetCore.Identity;

namespace Briefly.Infrastructure.Repositories
{
    public class RssSubscribRepository : GenericRepository<RssSubscription>, IRssSubscribRepository
    {
        private readonly UserManager<User> _userManager;
        public RssSubscribRepository(ApplicationDbContext context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public async Task<bool> CheckSubscribtionRss(int Userid, int Rssid)
        {
            var res = await _dbset.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == Userid && x.RssId == Rssid);
            return res==null? false:true;
        }
    }
}
