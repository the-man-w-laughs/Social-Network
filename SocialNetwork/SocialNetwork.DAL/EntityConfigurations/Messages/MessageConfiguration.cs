using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.EntityConfigurations.Messages;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(e => e.MessageId).HasName("PRIMARY");

        builder.ToTable("messages");

        builder.HasIndex(e => e.SenderId, "FK_messages_chat_members_idx");

        builder.HasIndex(e => e.ChatId, "FK_messages_chats_idx");

        builder.HasIndex(e => e.RepliedMessageId, "FK_messages_messages_idx");

        builder.HasIndex(e => e.MessageId, "message_id_UNIQUE").IsUnique();

        builder.Property(e => e.MessageId).HasColumnName("message_id");
        builder.Property(e => e.ChatId).HasColumnName("chat_id");
        builder.Property(e => e.Content)
            .HasColumnType("text")
            .HasColumnName("content");
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at");
        builder.Property(e => e.RepliedMessageId).HasColumnName("replied_message_id");
        builder.Property(e => e.SenderId).HasColumnName("sender_id");
        builder.Property(e => e.UpdatedAt)
            .HasMaxLength(45)
            .HasColumnName("updated_at");

        builder.HasOne(d => d.Chat).WithMany(p => p.Messages)
            .HasForeignKey(d => d.ChatId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_messages_chats");

        builder.HasOne(d => d.RepliedMessage).WithMany(p => p.InverseRepliedMessage)
            .HasForeignKey(d => d.RepliedMessageId)
            .HasConstraintName("FK_messages_messages");
    }
}
