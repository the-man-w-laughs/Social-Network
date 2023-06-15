using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.DAL.EntityConfigurations.Communities;

public class CommunityMemberTypeConfiguration : IEntityTypeConfiguration<CommunityMemberType>
{
    public void Configure(EntityTypeBuilder<CommunityMemberType> builder)
    {
        builder.HasKey(e => e.CommunityMemberTypeId).HasName("PRIMARY");

        builder.ToTable("community_member_types");

        builder.Property(e => e.CommunityMemberTypeId).HasColumnName("community_member_type_id");
        builder.Property(e => e.TypeName)
            .HasMaxLength(45)
            .HasColumnName("type_name");
    }
}
