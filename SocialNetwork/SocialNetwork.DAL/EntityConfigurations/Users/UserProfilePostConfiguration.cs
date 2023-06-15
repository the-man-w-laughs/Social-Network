using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Users;

public class UserProfilePostConfiguration : IEntityTypeConfiguration<UserProfilePost>
{
    public void Configure(EntityTypeBuilder<UserProfilePost> builder)
    {
        builder.HasKey(e => e.UserPostId).HasName("PRIMARY");

        builder.ToTable("user_profile_posts");

        builder.HasIndex(e => e.PostId, "FK_user_profile_posts_posts_idx");

        builder.HasIndex(e => e.UserId, "FK_user_profile_posts_users_idx");

        builder.Property(e => e.UserPostId)
            .ValueGeneratedNever()
            .HasColumnName("user_post_id");
        builder.Property(e => e.PostId).HasColumnName("post_id");
        builder.Property(e => e.UserId).HasColumnName("user_id");

        builder.HasOne(d => d.Post).WithMany(p => p.UserProfilePosts)
            .HasForeignKey(d => d.PostId)
            .HasConstraintName("FK_user_profile_posts_posts");

        builder.HasOne(d => d.User).WithMany(p => p.UserProfilePosts)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_user_profile_posts_users");
    }
}
