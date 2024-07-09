using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Briefly.Infrastructure.Configurationz
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Views).HasDefaultValue(0);
            builder.Property(x => x.Likes).HasDefaultValue(0);
            builder.Property(x => x.CommentsNumber).HasDefaultValue(0);
            builder.Property(x => x.ClusterLabel).HasDefaultValue(-1);

            builder.HasOne(x => x.RSS)
                   .WithMany(x => x.Articles)
                   .HasForeignKey(x => x.RSSId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.CommentArticles).
                    WithOne(p => p.Article).HasForeignKey(x => x.ArticleId);

            builder.HasMany(a => a.Clusters)
            .WithOne().HasForeignKey(c => c.ClusterLabel).OnDelete(DeleteBehavior.NoAction);
        }
        
    }
}
