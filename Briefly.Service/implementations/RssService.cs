using CodeHollow.FeedReader;
using Microsoft.AspNetCore.Identity;

namespace Briefly.Service.implementations
{
    public class RssService : IRssService
    {
        private readonly IRssRepository _rssRepository;
        private readonly IRssSubscribRepository _rssSubscribRepository;
        private readonly UserManager<User> _userManager;
        public RssService(IRssRepository rssRepository, IRssSubscribRepository rssSubscribRepository,
             UserManager<User> userManager)
        {
            _rssRepository = rssRepository;
            _rssSubscribRepository = rssSubscribRepository;
            _userManager = userManager;
        }

        public async Task<string> CreateRss(string rssUrl)
        {
            try
            {
                var rssFound = await _rssRepository.CheckRssExist(rssUrl);
                if (rssFound != null)
                {
                    return "RssFound";
                }
                var feed = await FeedReader.ReadAsync(rssUrl);
                var rss = new RSS()
                {
                    Description = feed.Description,
                    Image = feed.ImageUrl,
                    Link = rssUrl,
                    Title = feed.Title,
                };
                var rssCreated = await _rssRepository.AddRssAsync(rss);
                return "Added";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }

        public async Task<string> CreateUserRss(string userId, string url)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return "Usernotfound";
                }
                var rssFound = await _rssRepository.CheckRssExist(url); // true notfound false found 
                if (rssFound != null)
                {
                    var CheckSubscription = await _rssSubscribRepository.CheckSubscribtionRss(int.Parse(userId), rssFound.Id);
                    if (CheckSubscription)
                        return "usersubscribed";

                    var RssubScription = new RssSubscription()
                    {
                        RssId = rssFound.Id,
                        UserId = int.Parse(userId)
                    };
                    await _rssSubscribRepository.AddAsync(RssubScription);
                    return "Added";
                }
                var feed = await FeedReader.ReadAsync(url);
                var rss = new RSS()
                {
                    Description = feed.Description,
                    Image = feed.ImageUrl,
                    Link = url,
                    Title = feed.Title,
                };
                var rssCreated = await _rssRepository.AddRssAsync(rss);
                var RsssubScription = new RssSubscription()
                {
                    RssId = rssCreated.Id,
                    UserId = int.Parse(userId)
                };
                await _rssSubscribRepository.AddAsync(RsssubScription);
                return "Added";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }

        public async Task<List<RSS>> GetAllSubscribedRss(string userId)
        {
            var result = await _rssRepository.GetRssSubscribtions(userId);
            return result;
        }

        public async Task<string> RssUnUserSubscribe(string userId, int rssId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return "usernotfound";
                }
                var rss = await _rssRepository.GetByIdAsync(rssId);
                if (rss == null)
                {
                    return "rssnotfound";
                }
                var checkSubscription = await _rssSubscribRepository.CheckSubscribtionRss(int.Parse(userId), rssId);
                if (!checkSubscription)
                {
                    return "UserNotSubscribed";
                }
                var rsssubScription = new RssSubscription()
                {
                    RssId = rss.Id,
                    UserId = int.Parse(userId)
                };
                await _rssSubscribRepository.Delete(rsssubScription);
                return "deleted";
            }
            catch (Exception ex)
            {
                return "failed";
            }
        }


        public async Task<string> RssUserSubscribe(string userId, int rssId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return "usernotfound";
                }
                var rss = await _rssRepository.GetByIdAsync(rssId);
                if (rss == null)
                {
                    return "rssnotfound";
                }
                var checkSubscription = await _rssSubscribRepository.CheckSubscribtionRss(int.Parse(userId), rssId);
                if (checkSubscription)
                {
                    return "UserSubscrbedBefore";
                }
                var rssSubscribtion = new RssSubscription()
                {
                    RssId = rssId,
                    UserId = user.Id
                };
                await _rssSubscribRepository.AddAsync(rssSubscribtion);
                return "added";
            }
            catch (Exception ex)
            {
                return "failed";
            }
        }

        public async Task<List<RSS>> GetAllRss(string search)
        {
            if (search != null)
            {
                return await _rssRepository.GetTableNoTracking().Where(x => x.Title.ToLower().Contains(search.ToLower())).ToListAsync();
            }
            else
            {
                return await _rssRepository.GetTableNoTracking().ToListAsync();
            }
        }
        public async Task<RSS> GetRssById(int rssId)
        {
            var rss = await _rssRepository.GetByIdAsync(rssId);
            return rss;
        }

        public async Task UpdateRss(RSS rss)
        {
            await _rssRepository.UpdateAsync(rss);
        }

        public async Task<string> DeleteRss(RSS rss)
        {
            var res = _rssRepository.Delete(rss);
            if (!res.IsCompleted)
            {
                return "Fail";
            }
            return "Deleted Successfully";
        }
    }
}

