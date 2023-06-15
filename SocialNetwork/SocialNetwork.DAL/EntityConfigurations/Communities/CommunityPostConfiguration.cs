using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.DAL.EntityConfigurations.Communities;

public class CommunityPostConfiguration : IEntityTypeConfiguration<CommunityPost>
{
    public void Configure(EntityTypeBuilder<CommunityPost> builder)
    {
        builder.ToTable("community_posts");
        
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.HasIndex(e => e.CommunityId, "FK_community_posts_communities_idx");
        builder.HasIndex(e => e.PostId, "FK_community_posts_posts_idx");
        builder.HasIndex(e => e.ProposerId, "FK_community_posts_users_idx");

        builder.Property(e => e.Id).HasColumnName("id").IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(e => e.CommunityId).HasColumnName("community_id").IsRequired();
        builder.Property(e => e.PostId).HasColumnName("post_id").IsRequired();
        builder.Property(e => e.ProposerId).HasColumnName("proposer_id");

        builder.HasOne(d => d.Community).WithMany(p => p.CommunityPosts)
            .HasForeignKey(d => d.CommunityId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_community_posts_communities");

        builder.HasOne(d => d.Post).WithMany(p => p.CommunityPosts)
            .HasForeignKey(d => d.PostId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_community_posts_posts");

        builder.HasOne(d => d.Proposer).WithMany(p => p.CommunityPosts)
            .HasForeignKey(d => d.ProposerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_community_posts_users");
    }
}
