
namespace Briefly.Data.Entities
{
    public class RssSubscription
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int RssId { get; set; }
        public RSS Rss { get; set; }
        public DateTime DateSubscribed { get; set; } = DateTime.Now;

    }
}
