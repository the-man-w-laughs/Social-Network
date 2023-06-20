using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Comments;

namespace SocialNetwork.DAL.EntityConfigurations.Comments;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("comments");
        
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.HasIndex(e => e.AuthorId, "FK_comments_users_idx");
        builder.HasIndex(e => e.PostId, "FK_comments_posts_idx");
        builder.HasIndex(e => e.RepliedComment, "FK_comments_comments_idx");

        builder.Property(e => e.Id).HasColumnName("id").IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Content).HasColumnName("content")
            .HasColumnType("text");
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired()
            .HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at")
            .HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnUpdate();

        builder.Property(e => e.AuthorId).HasColumnName("author_id").IsRequired();
        builder.Property(e => e.PostId).HasColumnName("post_id").IsRequired();
        builder.Property(e => e.RepliedComment).HasColumnName("replied_comment");

        builder.HasOne(d => d.Author).WithMany(p => p.Comments)
            .HasForeignKey(d => d.AuthorId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_comments_users");

        builder.HasOne(d => d.Post).WithMany(p => p.Comments)
            .HasForeignKey(d => d.PostId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_comments_posts");
        
        builder.HasOne(d => d.RepliedCommentNavigation).WithMany(p => p.InverseRepliedCommentNavigation)
            .HasForeignKey(d => d.RepliedComment)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_comments_comments");
    }
}
