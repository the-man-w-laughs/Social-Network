using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.UserId).HasName("PRIMARY");

        builder.ToTable("users");

        builder.HasIndex(e => e.UserTypeId, "FK_users_user_types_idx");

        builder.Property(e => e.UserId)
            .ValueGeneratedNever()
            .HasColumnName("user_id");
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at");
        builder.Property(e => e.DeactivatedAt)
            .HasColumnType("datetime")
            .HasColumnName("deactivated_at");
        builder.Property(e => e.DeletedAt)
            .HasColumnType("datetime")
            .HasColumnName("deleted_at");
        builder.Property(e => e.IsDeactivated).HasColumnName("is_deactivated");
        builder.Property(e => e.IsDeleted).HasColumnName("is_deleted");
        builder.Property(e => e.LastActiveAt)
            .HasColumnType("datetime")
            .HasColumnName("last_active_at");
        builder.Property(e => e.Login)
            .HasMaxLength(20)
            .HasColumnName("login");
        builder.Property(e => e.Password)
            .HasMaxLength(32)
            .IsFixedLength()
            .HasColumnName("password");
        builder.Property(e => e.Salt)
            .HasMaxLength(20)
            .HasColumnName("salt");
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime")
            .HasColumnName("updated_at");
        builder.Property(e => e.UserTypeId).HasColumnName("user_type_id");

        builder.HasOne(d => d.UserType).WithMany(p => p.Users)
            .HasForeignKey(d => d.UserTypeId)
            .HasConstraintName("FK_users_user_types");
    }
}