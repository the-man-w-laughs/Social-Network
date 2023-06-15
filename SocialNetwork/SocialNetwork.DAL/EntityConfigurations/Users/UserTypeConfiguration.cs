using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Users;

public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
{
    public void Configure(EntityTypeBuilder<UserType> builder)
    {
        builder.HasKey(e => e.UserTypeId).HasName("PRIMARY");

        builder.ToTable("user_types");

        builder.Property(e => e.UserTypeId).HasColumnName("user_type_id");
        builder.Property(e => e.UserTypeName)
            .HasMaxLength(45)
            .HasColumnName("user_type_name");
    }
}
