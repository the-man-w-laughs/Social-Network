using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Medias;
using System.Reflection.Emit;

namespace SocialNetwork.DAL.EntityConfigurations.Medias;

public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.ToTable("medias");
        
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.Property(e => e.Id).HasColumnName("id").IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(e => e.FileName).HasColumnName("file_name").IsRequired()
            .HasMaxLength(Constants.MediaFileNameMaxLength);
        builder.Property(e => e.FilePath).HasColumnName("file_path").IsRequired()
            .HasMaxLength(Constants.MediaFilePathMaxLength);
        builder.Property(e => e.MediaTypeId).HasColumnName("media_type").IsRequired();
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired()
            .HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");        

        builder.HasOne(b => b.Owner)
            .WithMany(a => a.Medias)
            .HasForeignKey(b => b.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
