using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.DAL.EntityConfigurations.Chats;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(e => e.ChatId).HasName("PRIMARY");
        
        builder.ToTable("chats");
        
        builder.HasIndex(e => e.ChatId, "chat_id_UNIQUE").IsUnique();

        builder.Property(e => e.ChatId).HasColumnName("chat_id");
        builder.Property(e => e.CreatedAt)
            .HasMaxLength(45)
            .HasColumnName("created_at");
        builder.Property(e => e.Name)
            .HasMaxLength(45)
            .HasColumnName("name");
    }
}
