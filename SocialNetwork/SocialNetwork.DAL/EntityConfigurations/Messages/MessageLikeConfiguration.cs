using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.EntityConfigurations.Messages;

public class MessageLikeConfiguration : IEntityTypeConfiguration<MessageLike>
{
    public void Configure(EntityTypeBuilder<MessageLike> builder)
    {
        builder.ToTable("message_likes");
        
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.HasIndex(e => e.ChatMemberId, "FK_message_likes_chat_members_idx");
        builder.HasIndex(e => e.MessageId, "FK_message_likes_messages_idx");

        builder.Property(e => e.Id).HasColumnName("id").IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired()
            .HasColumnType("datetime");


        builder.Property(e => e.ChatMemberId).HasColumnName("chat_member_id").IsRequired();
        builder.Property(e => e.MessageId).HasColumnName("message_id").IsRequired();

        builder.HasOne(d => d.ChatMember).WithMany(p => p.MessageLikes)
            .HasForeignKey(d => d.ChatMemberId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_message_likes_chat_members");

        builder.HasOne(d => d.Message).WithMany(p => p.MessageLikes)
            .HasForeignKey(d => d.MessageId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_message_likes_messages");
    }
}
