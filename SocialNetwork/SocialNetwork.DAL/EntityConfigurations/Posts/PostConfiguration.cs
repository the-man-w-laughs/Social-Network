using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.DAL.EntityConfigurations.Posts;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(e => e.PostId).HasName("PRIMARY");

        builder.ToTable("posts");

        builder.HasIndex(e => e.RepostId, "FK_posts_posts_idx");

        builder.Property(e => e.PostId)
            .ValueGeneratedNever()
            .HasColumnName("post_id");
        builder.Property(e => e.Content)
            .HasMaxLength(45)
            .HasColumnName("content");
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at");
        builder.Property(e => e.RepostId).HasColumnName("repost_id");
        builder.Property(e => e.UpdatedAt)
            .HasMaxLength(45)
            .HasColumnName("updated_at");

        builder.HasOne(d => d.Repost).WithMany(p => p.InverseRepost)
            .HasForeignKey(d => d.RepostId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_posts_posts");
    }
}
