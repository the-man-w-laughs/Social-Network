using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Users;

public class UserFollowerConfiguration : IEntityTypeConfiguration<UserFollower>
{
    public void Configure(EntityTypeBuilder<UserFollower> builder)
    {
        builder.ToTable("user_followers");

        builder.HasKey(e => e.Id).HasName("PRIMARY");
        
        builder.HasIndex(e => e.SourceId, "FK_user_followers_source_users_idx");
        builder.HasIndex(e => e.TargetId, "FK_user_followers_target_users_idx");

        builder.Property(e => e.Id).HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();
        builder.Property(e => e.CreatedAt).HasColumnName("created_at")
            .HasColumnType("datetime")
            .IsRequired();
        builder.Property(e => e.SourceId).HasColumnName("source_id")
            .IsRequired();
        builder.Property(e => e.TargetId).HasColumnName("target_id")
            .IsRequired();

        builder.HasOne(d => d.Source).WithMany(p => p.UserFollowerSources)
            .HasForeignKey(d => d.SourceId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_user_followers_source_users");

        builder.HasOne(d => d.Target).WithMany(p => p.UserFollowerTargets)
            .HasForeignKey(d => d.TargetId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_user_followers_target_users");
    }
}
