using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.DAL.EntityConfigurations.Posts;

public class PostMediaConfiguration : IEntityTypeConfiguration<PostMedia>
{
    public void Configure(EntityTypeBuilder<PostMedia> builder)
    {
        builder.HasKey(e => e.PostMediaId).HasName("PRIMARY");

        builder.ToTable("post_medias");

        builder.HasIndex(e => e.MediaId, "FK_post_medias_medias_idx");

        builder.HasIndex(e => e.PostId, "FK_post_medias_posts_idx");

        builder.Property(e => e.PostMediaId)
            .ValueGeneratedNever()
            .HasColumnName("post_media_id");
        builder.Property(e => e.MediaId).HasColumnName("media_id");
        builder.Property(e => e.PostId).HasColumnName("post_id");

        builder.HasOne(d => d.Media).WithMany(p => p.PostMedia)
            .HasForeignKey(d => d.MediaId)
            .HasConstraintName("FK_post_medias_medias");

        builder.HasOne(d => d.Post).WithMany(p => p.PostMedia)
            .HasForeignKey(d => d.PostId)
            .HasConstraintName("FK_post_medias_posts");
    }
}