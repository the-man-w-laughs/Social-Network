using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Users;

public class UserFollowerConfiguration : IEntityTypeConfiguration<UserFollower>
{
    public void Configure(EntityTypeBuilder<UserFollower> builder)
    {
        builder.HasKey(e => e.UserFollowerId).HasName("PRIMARY");

        builder.ToTable("user_followers");

        builder.HasIndex(e => e.SourceId, "FK_user_followers_source_users_idx");

        builder.HasIndex(e => e.TargetId, "FK_user_followers_target_users_idx");

        builder.Property(e => e.UserFollowerId)
            .ValueGeneratedNever()
            .HasColumnName("user_follower_id");
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at");
        builder.Property(e => e.SourceId).HasColumnName("source_id");
        builder.Property(e => e.TargetId).HasColumnName("target_id");

        builder.HasOne(d => d.Source).WithMany(p => p.UserFollowerSources)
            .HasForeignKey(d => d.SourceId)
            .HasConstraintName("FK_user_followers_source_users");

        builder.HasOne(d => d.Target).WithMany(p => p.UserFollowerTargets)
            .HasForeignKey(d => d.TargetId)
            .HasConstraintName("FK_user_followers_target_users");
    }
}
