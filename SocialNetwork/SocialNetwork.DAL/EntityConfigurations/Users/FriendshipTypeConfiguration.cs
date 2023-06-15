using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Users;

public class FriendshipTypeConfiguration : IEntityTypeConfiguration<FriendshipType>
{
    public void Configure(EntityTypeBuilder<FriendshipType> builder)
    {
        builder.HasKey(e => e.FriendshipTypeId).HasName("PRIMARY");

        builder.ToTable("friendship_types");

        builder.Property(e => e.FriendshipTypeId).HasColumnName("friendship_type_id");
        builder.Property(e => e.FriendshipType1)
            .HasMaxLength(45)
            .HasColumnName("friendship_type");
    }
}