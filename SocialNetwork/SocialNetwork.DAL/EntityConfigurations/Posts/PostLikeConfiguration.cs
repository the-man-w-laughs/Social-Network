using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.DAL.EntityConfigurations.Posts;

public class PostLikeConfiguration : IEntityTypeConfiguration<PostLike>
{
    public void Configure(EntityTypeBuilder<PostLike> builder)
    {
        builder.HasKey(e => e.PostLikeId).HasName("PRIMARY");

        builder.ToTable("post_likes");

        builder.HasIndex(e => e.PostId, "FK_post_likes_posts_idx");

        builder.HasIndex(e => e.UserId, "FK_post_likes_users_idx");

        builder.Property(e => e.PostLikeId)
            .ValueGeneratedNever()
            .HasColumnName("post_like_id");
        builder.Property(e => e.PostId).HasColumnName("post_id");
        builder.Property(e => e.UserId).HasColumnName("user_id");

        builder.HasOne(d => d.Post).WithMany(p => p.PostLikes)
            .HasForeignKey(d => d.PostId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_post_likes_posts");

        builder.HasOne(d => d.User).WithMany(p => p.PostLikes)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_post_likes_users");
    }
}
