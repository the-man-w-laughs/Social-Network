using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.DAL.EntityConfigurations.Communities;

public class CommunityPostConfiguration : IEntityTypeConfiguration<CommunityPost>
{
    public void Configure(EntityTypeBuilder<CommunityPost> builder)
    {
        builder.HasKey(e => e.CommunityPostId).HasName("PRIMARY");

        builder.ToTable("community_posts");

        builder.HasIndex(e => e.CommunityId, "FK_community_posts_communities_idx");

        builder.HasIndex(e => e.PostId, "FK_community_posts_posts_idx");

        builder.HasIndex(e => e.ProposerId, "FK_community_posts_users_idx");

        builder.Property(e => e.CommunityPostId)
            .ValueGeneratedNever()
            .HasColumnName("community_post_id");
        builder.Property(e => e.CommunityId).HasColumnName("community_id");
        builder.Property(e => e.PostId).HasColumnName("post_id");
        builder.Property(e => e.ProposerId).HasColumnName("proposer_id");

        builder.HasOne(d => d.Community).WithMany(p => p.CommunityPosts)
            .HasForeignKey(d => d.CommunityId)
            .HasConstraintName("FK_community_posts_communities");

        builder.HasOne(d => d.Post).WithMany(p => p.CommunityPosts)
            .HasForeignKey(d => d.PostId)
            .HasConstraintName("FK_community_posts_posts");

        builder.HasOne(d => d.Proposer).WithMany(p => p.CommunityPosts)
            .HasForeignKey(d => d.ProposerId)
            .HasConstraintName("FK_community_posts_users");
    }
}
