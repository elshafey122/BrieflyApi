namespace Briefly.Infrastructure.Configuration
{
    public class UserConfiguration : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.ArticleSaved)
                .WithOne(sa => sa.User)
                .HasForeignKey(sa => sa.UserId).OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(u => u.PostsSaved)
            //    .WithOne(sp => sp.User)
            //    .HasForeignKey(sp => sp.UserId).OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(u => u.LikedPosts)
            //    .WithOne(pl => pl.User)
            //    .HasForeignKey(pl => pl.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.RssSubscriptions)
                       .WithOne(rss => rss.User)
                       .HasForeignKey(rss => rss.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.CommentsArticle)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired(true).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
