using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.DAL.EntityConfigurations.Medias;

public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.HasKey(e => e.MediaId).HasName("PRIMARY");

        builder.ToTable("medias");

        builder.HasIndex(e => e.MediaType, "FK_medias_media_types_idx");

        builder.Property(e => e.MediaId)
            .ValueGeneratedNever()
            .HasColumnName("media_id");
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at");
        builder.Property(e => e.EntityType)
            .HasMaxLength(45)
            .HasColumnName("entity_type");
        builder.Property(e => e.FileName)
            .HasMaxLength(255)
            .HasColumnName("file_name");
        builder.Property(e => e.FilePath)
            .HasMaxLength(1024)
            .HasColumnName("file_path");
        builder.Property(e => e.MediaType).HasColumnName("media_type");
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime")
            .HasColumnName("updated_at");

        builder.HasOne(d => d.MediaTypeNavigation).WithMany(p => p.Media)
            .HasForeignKey(d => d.MediaType)
            .HasConstraintName("FK_medias_media_types");
    }
}
