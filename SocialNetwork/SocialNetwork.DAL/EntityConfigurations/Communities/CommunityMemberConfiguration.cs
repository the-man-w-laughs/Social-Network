using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.DAL.EntityConfigurations.Communities;

public class CommunityMemberConfiguration : IEntityTypeConfiguration<CommunityMember>
{
    public void Configure(EntityTypeBuilder<CommunityMember> builder)
    {
        builder.HasKey(e => e.CommunityMemberId).HasName("PRIMARY");

        builder.ToTable("community_members");

        builder.HasIndex(e => e.CommunityId, "FK_community_members_communities_idx");

        builder.HasIndex(e => e.CommunityMemberTypeId, "FK_community_members_community_member_types_idx");

        builder.HasIndex(e => e.UserId, "FK_community_members_users_idx");

        builder.Property(e => e.CommunityMemberId)
            .ValueGeneratedNever()
            .HasColumnName("community_member_id");
        builder.Property(e => e.CommunityId).HasColumnName("community_id");
        builder.Property(e => e.CommunityMemberTypeId).HasColumnName("community_member_type_id");
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at");
        builder.Property(e => e.UserId).HasColumnName("user_id");

        builder.HasOne(d => d.Community).WithMany(p => p.CommunityMembers)
            .HasForeignKey(d => d.CommunityId)
            .HasConstraintName("FK_community_members_communities");

        builder.HasOne(d => d.CommunityMemberType).WithMany(p => p.CommunityMembers)
            .HasForeignKey(d => d.CommunityMemberTypeId)
            .HasConstraintName("FK_community_members_community_member_types");

        builder.HasOne(d => d.User).WithMany(p => p.CommunityMembers)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_community_members_users");
    }
}
