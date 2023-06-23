using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Users;

public class UserProfilePostConfiguration : IEntityTypeConfiguration<UserProfilePost>
{
    public void Configure(EntityTypeBuilder<UserProfilePost> builder)
    {
        builder.ToTable("user_profile_posts");
     
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        
        builder.HasIndex(e => e.PostId, "FK_user_profile_posts_posts_idx");
        builder.HasIndex(e => e.UserId, "FK_user_profile_posts_users_idx");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();
        builder.Property(e => e.PostId)
            .HasColumnName("post_id")
            .IsRequired();
        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        
        builder.HasOne(d => d.Post).WithMany(p => p.UserProfilePosts)
            .HasForeignKey(d => d.PostId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_user_profile_posts_posts");

        builder.HasOne(d => d.User).WithMany(p => p.UserProfilePosts)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_user_profile_posts_users");
    }
}
