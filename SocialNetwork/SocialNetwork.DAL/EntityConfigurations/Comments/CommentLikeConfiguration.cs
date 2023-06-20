using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Comments;

namespace SocialNetwork.DAL.EntityConfigurations.Comments;

public class CommentLikeConfiguration : IEntityTypeConfiguration<CommentLike>
{
    public void Configure(EntityTypeBuilder<CommentLike> builder)
    {
        builder.ToTable("comment_likes");
        
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        
        builder.HasIndex(e => e.CommentId, "FK_comment_likes_comments_idx");
        builder.HasIndex(e => e.UserId, "FK_comment_likes_users_idx");

        builder.Property(e => e.Id).HasColumnName("id").IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired()
            .HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(e => e.CommentId).HasColumnName("comment_id").IsRequired();
        builder.Property(e => e.UserId).HasColumnName("user_id").IsRequired();

        builder.HasOne(d => d.Comment).WithMany(p => p.CommentLikes)
            .HasForeignKey(d => d.CommentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_comment_likes_comments");

        builder.HasOne(d => d.User).WithMany(p => p.CommentLikes)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_comment_likes_users");
    }
}
