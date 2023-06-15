using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.DAL.EntityConfigurations.Medias;

public class MediaTypeConfiguration : IEntityTypeConfiguration<MediaType>
{
    public void Configure(EntityTypeBuilder<MediaType> builder)
    {
        builder.HasKey(e => e.MediaTypeId).HasName("PRIMARY");

        builder.ToTable("media_types");

        builder.Property(e => e.MediaTypeId).HasColumnName("media_type_id");
        builder.Property(e => e.MediaType1)
            .HasMaxLength(45)
            .HasColumnName("media_type");
    }
}
