using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace Briefly.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<RSS> RSSes { get; set; }
        public DbSet<RssSubscription> RssSubscriptions { get; set; }
        public DbSet<UserRefreshToken> RefreshToken { get; set; }
        public DbSet<SavedArticle> SavedArticles { get; set; }
        public DbSet<Comment> CommentsArticle { get; set; }


        //public DbSet<PostComment> Comments { get; set; }
        //public DbSet<UserFollowing> Followings { get; set; }
        //public DbSet<Post> Posts { get; set; }
        //public DbSet<PostLike> PostLikes { get; set; }
        //public DbSet<SavedPost> SavedPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
