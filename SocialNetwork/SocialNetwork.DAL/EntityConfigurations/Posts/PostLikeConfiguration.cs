using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.DAL.EntityConfigurations.Posts;

public class PostLikeConfiguration : IEntityTypeConfiguration<PostLike>
{
    public void Configure(EntityTypeBuilder<PostLike> builder)
    {
        builder.ToTable("post_likes");

        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.HasIndex(e => e.PostId, "FK_post_likes_posts_idx");
        builder.HasIndex(e => e.UserId, "FK_post_likes_users_idx");

        builder.Property(e => e.Id).HasColumnName("post_like_id").IsRequired()
            .ValueGeneratedOnAdd();
        
        builder.Property(e => e.PostId).HasColumnName("post_id").IsRequired();
        builder.Property(e => e.UserId).HasColumnName("user_id").IsRequired();

        builder.HasOne(d => d.Post).WithMany(p => p.PostLikes)
            .HasForeignKey(d => d.PostId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_post_likes_posts");

        builder.HasOne(d => d.User).WithMany(p => p.PostLikes)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_post_likes_users");
    }
}
