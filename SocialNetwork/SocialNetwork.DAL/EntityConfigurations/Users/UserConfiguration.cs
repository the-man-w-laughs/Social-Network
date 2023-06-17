using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        
        builder.Property(e => e.Id).HasColumnName("id").IsRequired()
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Login).HasColumnName("login").IsRequired()
            .HasMaxLength(Constants.UserLoginMaxLength);
        builder.Property(e => e.Email).HasColumnName("email").IsRequired()
            .HasMaxLength(Constants.UserEmailMaxLength);
        builder.Property(e => e.PasswordHash).HasColumnName("Password").IsRequired()
            .HasMaxLength(32)
            .IsFixedLength();
        builder.Property(e => e.Salt).HasColumnName("salt").IsRequired()
            .HasMaxLength(20);
        builder.Property(e => e.TypeId).HasColumnName("type_id").IsRequired();
        builder.Property(e => e.LastActiveAt).HasColumnName("last_active_at").IsRequired()
            .HasColumnType("datetime");
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired()
            .HasColumnType("datetime");
        builder.Property(e => e.DeletedAt).HasColumnName("deleted_at")
            .HasColumnType("datetime");
        builder.Property(e => e.DeactivatedAt).HasColumnName("deactivated_at")
            .HasColumnType("datetime");
        builder.Property(e => e.UserTypeUpdatedAt).HasColumnName("user_type_updated_at")
            .HasColumnType("datetime");
        builder.Property(e => e.LoginUpdatedAt).HasColumnName("login_updated_at")
            .HasColumnType("datetime");
        builder.Property(e => e.EmailUpdatedAt).HasColumnName("email_updated_at")
            .HasColumnType("datetime");
        builder.Property(e => e.PasswordUpdatedAt).HasColumnName("Password_updated_at")
            .HasColumnType("datetime");
        builder.Property(e => e.IsDeactivated).HasColumnName("is_deactivated");
        builder.Property(e => e.IsDeleted).HasColumnName("is_deleted");
        
    }
}