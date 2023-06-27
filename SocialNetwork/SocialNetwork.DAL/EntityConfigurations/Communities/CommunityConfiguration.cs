using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Communities;

public class CommunityConfiguration : IEntityTypeConfiguration<Community>
{
    public void Configure(EntityTypeBuilder<Community> builder)
    {
        builder.ToTable("communities");
        
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.Property(e => e.Id).HasColumnName("id").IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name).HasColumnName("name").IsRequired()
            .HasMaxLength(Constants.CommunityNameMaxLength);
        builder.Property(e => e.Description).HasColumnName("description")
            .HasColumnType("text");
        builder.Property(e => e.IsPrivate).HasColumnName("is_private").IsRequired();
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired()
            .HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime");
        builder.Property(e => e.CommunityPictureId)
    .HasColumnName("community_picture_id");
        builder.HasOne(up => up.CommunityPicture).WithOne(u => u.Community)
            .HasForeignKey<Community>(up => up.CommunityPictureId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_picture_community_picture");
    }
}
