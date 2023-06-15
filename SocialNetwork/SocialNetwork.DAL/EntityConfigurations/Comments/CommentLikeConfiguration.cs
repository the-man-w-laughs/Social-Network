using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Comments;

namespace SocialNetwork.DAL.EntityConfigurations.Comments;

public class CommentLikeConfiguration : IEntityTypeConfiguration<CommentLike>
{
    public void Configure(EntityTypeBuilder<CommentLike> builder)
    {
        builder.HasKey(e => e.CommentLikeId).HasName("PRIMARY");

        builder.ToTable("comment_likes");

        builder.HasIndex(e => e.CommentId, "FK_comment_likes_comments_idx");

        builder.HasIndex(e => e.UserId, "FK_comment_likes_users_idx");

        builder.Property(e => e.CommentLikeId)
            .ValueGeneratedNever()
            .HasColumnName("comment_like_id");
        builder.Property(e => e.CommentId).HasColumnName("comment_id");
        builder.Property(e => e.UserId).HasColumnName("user_id");

        builder.HasOne(d => d.Comment).WithMany(p => p.CommentLikes)
            .HasForeignKey(d => d.CommentId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_comment_likes_comments");

        builder.HasOne(d => d.User).WithMany(p => p.CommentLikes)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_comment_likes_users");
    }
}
