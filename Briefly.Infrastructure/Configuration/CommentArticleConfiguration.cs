
namespace Briefly.Infrastructure.Configuration
{
    public class CommentArticleConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ParentComment)
                .WithMany(x => x.Replies)
                .HasForeignKey(x => x.ParentCommentId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired(false);
           
            builder.Property(x => x.Text).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Likes).HasDefaultValue(0);

        }
    }
}
