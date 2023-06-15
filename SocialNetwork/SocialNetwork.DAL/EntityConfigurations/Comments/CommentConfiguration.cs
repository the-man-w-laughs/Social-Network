using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Comments;

namespace SocialNetwork.DAL.EntityConfigurations.Comments;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(e => e.CommentId).HasName("PRIMARY");

        builder.ToTable("comments");

        builder.HasIndex(e => e.RepliedComment, "FK_comments_comments_idx");

        builder.HasIndex(e => e.PostId, "FK_comments_posts_idx");

        builder.HasIndex(e => e.AuthorId, "FK_comments_users_idx");

        builder.Property(e => e.CommentId)
            .ValueGeneratedNever()
            .HasColumnName("comment_id");
        builder.Property(e => e.AuthorId).HasColumnName("author_id");
        builder.Property(e => e.Content)
            .HasColumnType("text")
            .HasColumnName("content");
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at");
        builder.Property(e => e.PostId).HasColumnName("post_id");
        builder.Property(e => e.RepliedComment).HasColumnName("replied_comment");
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime")
            .HasColumnName("updated_at");

        builder.HasOne(d => d.Author).WithMany(p => p.Comments)
            .HasForeignKey(d => d.AuthorId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_comments_users");

        builder.HasOne(d => d.Post).WithMany(p => p.Comments)
            .HasForeignKey(d => d.PostId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_comments_posts");

        builder.HasOne(d => d.RepliedCommentNavigation).WithMany(p => p.InverseRepliedCommentNavigation)
            .HasForeignKey(d => d.RepliedComment)
            .HasConstraintName("FK_comments_comments");
    }
}
