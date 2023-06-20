using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Users;

public class UserFriendConfiguration : IEntityTypeConfiguration<UserFriend>
{
    public void Configure(EntityTypeBuilder<UserFriend> builder)
    {
        builder.ToTable("user_friends");
        
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        
        builder.HasIndex(e => e.User1Id, "FK_user_friends_users_1_idx");
        builder.HasIndex(e => e.User2Id, "FK_user_friends_users_2_idx");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime")
            .IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at")
            .HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnUpdate();
        builder.Property(e => e.FriendshipTypeId)
            .HasColumnName("friendship_type")
            .IsRequired();
        builder.Property(e => e.User1Id)
            .HasColumnName("user_1_id")
            .IsRequired();
        builder.Property(e => e.User2Id)
            .HasColumnName("user_2_id")
            .IsRequired();

        builder.HasOne(d => d.User1).WithMany(p => p.UserFriendUser1s)
            .HasForeignKey(d => d.User1Id)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_user_friends_users_1");

        builder.HasOne(d => d.User2).WithMany(p => p.UserFriendUser2s)
            .HasForeignKey(d => d.User2Id)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_user_friends_users_2");
    }
}
