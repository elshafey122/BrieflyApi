using Briefly.Data.Entities;

namespace Briefly.Data.Identity
{
    public class User:IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Code { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        //public int SubscriptionId { get; set; }
        //public SubscriptionUser SubscriptionUser { get; set; }

        // not handled yet
        public ICollection<SavedArticle> ArticleSaved { get; set; } = new HashSet<SavedArticle>();

        public ICollection<RssSubscription> RssSubscriptions { get; set; } = new HashSet<RssSubscription>();

        public ICollection<Comment> CommentsArticle { get; set; } = new HashSet<Comment>();

        public virtual ICollection<UserRefreshToken>? RefreshTokens { get; set; }

    }
}