using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Users;

public class UserFriendConfiguration : IEntityTypeConfiguration<UserFriend>
{
    public void Configure(EntityTypeBuilder<UserFriend> builder)
    {
        builder.HasKey(e => e.UserFriendId).HasName("PRIMARY");

        builder.ToTable("user_friends");

        builder.HasIndex(e => e.FriendshipType, "FK_user_friends_friendship_types_idx");

        builder.HasIndex(e => e.User1Id, "FK_user_friends_users_1_idx");

        builder.HasIndex(e => e.User2Id, "FK_user_friends_users_2_idx");

        builder.Property(e => e.UserFriendId)
            .ValueGeneratedNever()
            .HasColumnName("user_friend_id");
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at");
        builder.Property(e => e.FriendshipType).HasColumnName("friendship_type");
        builder.Property(e => e.User1Id).HasColumnName("user_1_id");
        builder.Property(e => e.User2Id).HasColumnName("user_2_id");

        builder.HasOne(d => d.FriendshipTypeNavigation).WithMany(p => p.UserFriends)
            .HasForeignKey(d => d.FriendshipType)
            .HasConstraintName("FK_user_friends_friendship_types");

        builder.HasOne(d => d.User1).WithMany(p => p.UserFriendUser1s)
            .HasForeignKey(d => d.User1Id)
            .HasConstraintName("FK_user_friends_users_1");

        builder.HasOne(d => d.User2).WithMany(p => p.UserFriendUser2s)
            .HasForeignKey(d => d.User2Id)
            .HasConstraintName("FK_user_friends_users_2");
    }
}
