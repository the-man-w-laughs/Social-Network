using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(user => user.Id).HasName("PRIMARY");
        
        builder.Property(user  => user.Id).HasColumnName("id").IsRequired()
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(user  => user.Login).HasColumnName("login").IsRequired()
            .HasMaxLength(Constants.UserLoginMaxLength);
        builder.Property(user  => user.Email).HasColumnName("email").IsRequired()
            .HasMaxLength(Constants.UserEmailMaxLength);
        builder.Property(user  => user.PasswordHash).HasColumnName("password").IsRequired()
            .HasMaxLength(32)
            .IsFixedLength();
        builder.Property(user  => user.Salt).HasColumnName("salt").IsRequired()
            .HasMaxLength(20);
        builder.Property(user  => user.TypeId).HasColumnName("type_id").IsRequired();
        builder.Property(user  => user.LastActiveAt).HasColumnName("last_active_at").IsRequired()
            .HasColumnType("datetime");
        builder.Property(user  => user.CreatedAt).HasColumnName("created_at").IsRequired()
            .HasColumnType("datetime");
        builder.Property(user  => user.DeletedAt).HasColumnName("deleted_at")
            .HasColumnType("datetime");
        builder.Property(user  => user.DeactivatedAt).HasColumnName("deactivated_at")
            .HasColumnType("datetime");
        builder.Property(user  => user.UserTypeUpdatedAt).HasColumnName("user_type_updated_at")
            .HasColumnType("datetime");
        builder.Property(user  => user.LoginUpdatedAt).HasColumnName("login_updated_at")
            .HasColumnType("datetime");
        builder.Property(user  => user.EmailUpdatedAt).HasColumnName("email_updated_at")
            .HasColumnType("datetime");
        builder.Property(user  => user.PasswordUpdatedAt).HasColumnName("Password_updated_at")
            .HasColumnType("datetime");
        builder.Property(user  => user.IsDeactivated).HasColumnName("is_deactivated");
        builder.Property(user  => user.IsDeleted).HasColumnName("is_deleted");
        
    }
}