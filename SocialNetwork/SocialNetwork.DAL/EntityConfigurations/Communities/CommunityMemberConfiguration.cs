using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.DAL.EntityConfigurations.Communities;

public class CommunityMemberConfiguration : IEntityTypeConfiguration<CommunityMember>
{
    public void Configure(EntityTypeBuilder<CommunityMember> builder)
    {
        builder.ToTable("community_members");
        
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.HasIndex(e => e.CommunityId, "FK_community_members_communities_idx");
        builder.HasIndex(e => e.UserId, "FK_community_members_users_idx");

        builder.Property(e => e.Id).HasColumnName("community_member_id").IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(e => e.TypeId).HasColumnName("community_member_type").IsRequired();
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired()
            .HasColumnType("datetime");
        
        builder.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(e => e.CommunityId).HasColumnName("community_id").IsRequired();

        builder.HasOne(d => d.Community).WithMany(p => p.CommunityMembers)
            .HasForeignKey(d => d.CommunityId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_community_members_communities");

        builder.HasOne(d => d.User).WithMany(p => p.CommunityMembers)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_community_members_users");
    }
}
