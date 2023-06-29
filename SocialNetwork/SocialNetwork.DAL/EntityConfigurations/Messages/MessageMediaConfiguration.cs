using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.EntityConfigurations.Messages;

public class MessageMediaConfiguration : IEntityTypeConfiguration<MessageMedia>
{
    public void Configure(EntityTypeBuilder<MessageMedia> builder)
    {
        builder.ToTable("message_medias");

        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.HasIndex(e => e.MediaId, "FK_message_medias_medias_idx");
        builder.HasIndex(e => e.MessageId, "FK_message_medias_messages_idx");
        builder.HasIndex(e => e.ChatId, "FK_message_medias_chats_idx");

        builder.Property(e => e.Id).HasColumnName("id").IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(e => e.MediaId).HasColumnName("media_id").IsRequired();
        builder.Property(e => e.MessageId).HasColumnName("message_id").IsRequired();
        builder.Property(e => e.ChatId).HasColumnName("chat_id").IsRequired();

        builder.HasOne(d => d.Media).WithMany(p => p.MessageMedia)
            .HasForeignKey(d => d.MediaId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_message_medias_medias");

        builder.HasOne(d => d.Message).WithMany(p => p.Attachments)
            .HasForeignKey(d => d.MessageId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_message_medias_messages");

        builder.HasOne(d => d.Chat).WithMany(p => p.ChatMedias)
            .HasForeignKey(d => d.ChatId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_message_medias_chats");
    }
}
