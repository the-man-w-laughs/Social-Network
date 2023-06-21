using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.DAL.EntityConfigurations.Chats;

public class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMember>
{
    public void Configure(EntityTypeBuilder<ChatMember> builder)
    {
        builder.ToTable("chat_members");
        
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.HasIndex(e => e.Id, "user_chat_id_UNIQUE").IsUnique();
        builder.HasIndex(e => e.UserId, "FK_chat_members_users_idx");
        builder.HasIndex(e => e.ChatId, "FK_chat_members_chats_idx");

        builder.Property(e => e.Id).HasColumnName("id").IsRequired();
        builder.Property(e => e.TypeId).HasColumnName("chat_member_type").IsRequired();
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired()
            .HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at")
            .HasColumnType("datetime");

        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.ChatId).HasColumnName("chat_id");

        builder.HasOne(d => d.User).WithMany(p => p.ChatMembers)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_chat_members_users");

        builder.HasOne(d => d.Chat).WithMany(p => p.ChatMembers)
            .HasForeignKey(d => d.ChatId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_chat_members_chats");
    }
}
