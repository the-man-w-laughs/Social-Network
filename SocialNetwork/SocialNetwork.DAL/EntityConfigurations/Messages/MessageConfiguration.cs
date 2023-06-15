using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.EntityConfigurations.Messages;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages");
        
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.HasIndex(e => e.Id, "message_id_UNIQUE").IsUnique();
        builder.HasIndex(e => e.ChatId, "FK_messages_chats_idx");
        builder.HasIndex(e => e.SenderId, "FK_messages_chat_members_idx");
        builder.HasIndex(e => e.RepliedMessageId, "FK_messages_messages_idx");

        builder.Property(e => e.Id).HasColumnName("id").IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Content).HasColumnName("content")
            .HasColumnType("text").HasMaxLength(Constants.MessageTextMaxLength);
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired()
            .HasColumnType("datetime");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        
        builder.Property(e => e.ChatId).HasColumnName("chat_id").IsRequired();
        builder.Property(e => e.SenderId).HasColumnName("sender_id").IsRequired();
        builder.Property(e => e.RepliedMessageId).HasColumnName("replied_message_id");

        builder.HasOne(d => d.Chat).WithMany(p => p.Messages)
            .HasForeignKey(d => d.ChatId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_messages_chats");

        builder.HasOne(d => d.RepliedMessage).WithMany(p => p.InverseRepliedMessage)
            .HasForeignKey(d => d.RepliedMessageId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_messages_messages");
    }
}
