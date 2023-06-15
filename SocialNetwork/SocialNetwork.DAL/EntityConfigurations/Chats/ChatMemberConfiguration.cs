using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.DAL.EntityConfigurations.Chats;

public class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMember>
{
    public void Configure(EntityTypeBuilder<ChatMember> builder)
    {
        builder.HasKey(e => e.UserChatId).HasName("PRIMARY");

        builder.ToTable("chat_members");

        builder.HasIndex(e => e.ChatMemberType, "FK_chat_members_chat_members_types_idx");

        builder.HasIndex(e => e.ChatId, "FK_chat_members_chats_idx");

        builder.HasIndex(e => e.UserId, "FK_chat_members_users_idx");

        builder.HasIndex(e => e.UserChatId, "user_chat_id_UNIQUE").IsUnique();

        builder.Property(e => e.UserChatId).HasColumnName("user_chat_id");
        builder.Property(e => e.ChatId).HasColumnName("chat_id");
        builder.Property(e => e.ChatMemberType).HasColumnName("chat_member_type");
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at");
        builder.Property(e => e.UserId).HasColumnName("user_id");

        builder.HasOne(d => d.Chat).WithMany(p => p.ChatMembers)
            .HasForeignKey(d => d.ChatId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_chat_members_chats");

        builder.HasOne(d => d.ChatMemberTypeNavigation).WithMany(p => p.ChatMembers)
            .HasForeignKey(d => d.ChatMemberType)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_chat_members_chat_members_types");

        builder.HasOne(d => d.User).WithMany(p => p.ChatMembers)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_chat_members_users");
    }
}
