using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.DAL.EntityConfigurations.Communities;

public class CommunityConfiguration : IEntityTypeConfiguration<Community>
{
    public void Configure(EntityTypeBuilder<Community> builder)
    {
        builder.HasKey(e => e.CommunityId).HasName("PRIMARY");

        builder.ToTable("communities");

        builder.Property(e => e.CommunityId)
            .ValueGeneratedNever()
            .HasColumnName("community_id");
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at");
        builder.Property(e => e.Description)
            .HasColumnType("text")
            .HasColumnName("description");
        builder.Property(e => e.IsPrivate).HasColumnName("is_private");
        builder.Property(e => e.Name)
            .HasMaxLength(45)
            .HasColumnName("name");
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime")
            .HasColumnName("updated_at");
    }
}
