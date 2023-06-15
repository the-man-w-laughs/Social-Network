using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Comments;

namespace SocialNetwork.DAL.EntityConfigurations.Comments;

public class CommentMediaConfiguration : IEntityTypeConfiguration<CommentMedia>
{
    public void Configure(EntityTypeBuilder<CommentMedia> builder)
    {
        builder.HasKey(e => e.CommentMediaId).HasName("PRIMARY");

        builder.ToTable("comment_medias");

        builder.HasIndex(e => e.CommentId, "FK_comment_medias_comments_idx");

        builder.HasIndex(e => e.MediaId, "FK_comment_medias_medias_idx");

        builder.Property(e => e.CommentMediaId)
            .ValueGeneratedNever()
            .HasColumnName("comment_media_id");
        builder.Property(e => e.CommentId).HasColumnName("comment_id");
        builder.Property(e => e.MediaId).HasColumnName("media_id");

        builder.HasOne(d => d.Comment).WithMany(p => p.CommentMedia)
            .HasForeignKey(d => d.CommentId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_comment_medias_comments");

        builder.HasOne(d => d.Media).WithMany(p => p.CommentMedia)
            .HasForeignKey(d => d.MediaId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_comment_medias_medias");
    }
}
