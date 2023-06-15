using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.EntityConfigurations.Messages;

public class MessageMediaConfiguration : IEntityTypeConfiguration<MessageMedia>
{
    public void Configure(EntityTypeBuilder<MessageMedia> builder)
    {
        builder.HasKey(e => e.MessageMediaId).HasName("PRIMARY");

        builder.ToTable("message_medias");

        builder.HasIndex(e => e.ChatId, "FK_message_medias_chats_idx");

        builder.HasIndex(e => e.MediaId, "FK_message_medias_medias_idx");

        builder.HasIndex(e => e.MessageId, "FK_message_medias_messages_idx");

        builder.Property(e => e.MessageMediaId)
            .ValueGeneratedNever()
            .HasColumnName("message_media_id");
        builder.Property(e => e.ChatId).HasColumnName("chat_id");
        builder.Property(e => e.MediaId).HasColumnName("media_id");
        builder.Property(e => e.MessageId).HasColumnName("message_id");

        builder.HasOne(d => d.Chat).WithMany(p => p.MessageMedia)
            .HasForeignKey(d => d.ChatId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_message_medias_chats");

        builder.HasOne(d => d.Media).WithMany(p => p.MessageMedia)
            .HasForeignKey(d => d.MediaId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_message_medias_medias");

        builder.HasOne(d => d.Message).WithMany(p => p.MessageMedia)
            .HasForeignKey(d => d.MessageId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_message_medias_messages");
    }
}
