using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Users;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("user_profiles");
        
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        
        builder.HasIndex(e => e.UserId, "FK_user_profiles_users_idx");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();
        builder.Property(e => e.UserEmail)
            .HasColumnName("user_email")
            .HasMaxLength(Constants.EmailMaxLength);
        builder.Property(e => e.UserName)
            .HasColumnName("user_name")
            .HasMaxLength(Constants.UserNameMaxLength);
        builder.Property(e => e.UserSurname)
            .HasColumnName("user_surname")
            .HasMaxLength(Constants.UserSurnameMaxLength);
        builder.Property(e => e.UserSex)
            .HasColumnName("user_sex")
            .HasMaxLength(Constants.UserSexMaxLength);
        builder.Property(e => e.UserCountry)
            .HasColumnName("user_country")
            .HasMaxLength(Constants.CountryNameMaxLength);
        builder.Property(e => e.UserEducation)
            .HasColumnName("user_education")
            .HasMaxLength(Constants.UserEducationMaxLength);
        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime")
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime");
        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .IsRequired();
    
        builder.HasOne(d => d.User).WithMany(p => p.UserProfiles)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_user_profiles_users");
    }
}
