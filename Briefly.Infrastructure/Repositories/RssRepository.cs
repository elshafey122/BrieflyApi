
namespace Briefly.Infrastructure.Repositories
{
    public class RssRepository : GenericRepository<RSS>, IRssRepository
    {
        public RssRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<RSS> AddRssAsync(RSS rss)
        {
            var result = await _dbset.AddAsync(rss);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<RSS>> GetRssSubscribtions(string userId)
        {
            var Userid = int.Parse(userId);
            var res = await _dbset
            .Include(x => x.SubscribedBy)
            .Where(x => x.SubscribedBy.Any(s => s.UserId == Userid))
            .ToListAsync();
            return res;
        }

        public async Task<RSS> CheckRssExist(string Rssurl)
        {
            var rss = await _dbset.FirstOrDefaultAsync(x => x.Link == Rssurl);
            return rss;
        }
    }
}
