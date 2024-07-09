namespace Briefly.Infrastructure.Configuration
{
    public class RssConfiguration : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<RSS>
    {
        public void Configure(EntityTypeBuilder<RSS> builder)
        {
            builder.HasKey(rss => rss.Id);
            builder.Property(rss => rss.Id).ValueGeneratedOnAdd();
            builder.Property(rss => rss.Link).IsRequired();
            builder.Property(rss => rss.Title).IsRequired();
            builder.Property(rss => rss.Description).IsRequired();

            builder.HasMany(rss => rss.Articles)
                .WithOne(a => a.RSS)
                .HasForeignKey(a => a.RSSId).OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(rss => rss.SubscribedBy)
            //    .WithOne(s => s.Rss)
            //    .HasForeignKey(s => s.RssId);

        }
    }
}
