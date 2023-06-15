using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Users;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(e => e.UserProfileId).HasName("PRIMARY");

        builder.ToTable("user_profiles");

        builder.HasIndex(e => e.UserId, "FK_user_profiles_users_idx");

        builder.Property(e => e.UserProfileId)
            .ValueGeneratedNever()
            .HasColumnName("user_profile_id");
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at");
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime")
            .HasColumnName("updated_at");
        builder.Property(e => e.UserCountry)
            .HasMaxLength(45)
            .HasColumnName("user_country");
        builder.Property(e => e.UserEducation)
            .HasMaxLength(45)
            .HasColumnName("user_education");
        builder.Property(e => e.UserEmail)
            .HasMaxLength(100)
            .HasColumnName("user_email");
        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.UserName)
            .HasMaxLength(45)
            .HasColumnName("user_name");
        builder.Property(e => e.UserSex)
            .HasMaxLength(45)
            .HasColumnName("user_sex");
        builder.Property(e => e.UserSurname)
            .HasMaxLength(45)
            .HasColumnName("user_surname");

        builder.HasOne(d => d.User).WithMany(p => p.UserProfiles)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_user_profiles_users");
    }
}
