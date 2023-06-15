using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.EntityConfigurations.Messages;

public class MessageLikeConfiguration : IEntityTypeConfiguration<MessageLike>
{
    public void Configure(EntityTypeBuilder<MessageLike> builder)
    {
        builder.HasKey(e => e.MessageLikeId).HasName("PRIMARY");

        builder.ToTable("message_likes");

        builder.HasIndex(e => e.ChatMemberId, "FK_message_likes_chat_members_idx");

        builder.HasIndex(e => e.MessageId, "FK_message_likes_messages_idx");

        builder.Property(e => e.MessageLikeId)
            .ValueGeneratedNever()
            .HasColumnName("message_like_id");
        builder.Property(e => e.ChatMemberId).HasColumnName("chat_member_id");
        builder.Property(e => e.MessageId).HasColumnName("message_id");

        builder.HasOne(d => d.ChatMember).WithMany(p => p.MessageLikes)
            .HasForeignKey(d => d.ChatMemberId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_message_likes_chat_members");

        builder.HasOne(d => d.Message).WithMany(p => p.MessageLikes)
            .HasForeignKey(d => d.MessageId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_message_likes_messages");
    }
}
