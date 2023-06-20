using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.DAL.EntityConfigurations.Chats;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("chats");
        
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        
        builder.HasIndex(e => e.Id, "chat_id_UNIQUE").IsUnique();

        builder.Property(e => e.Id).HasColumnName("id").IsRequired();
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired()
            .HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at")
            .HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnUpdate();
        builder.Property(e => e.Name).HasColumnName("name").IsRequired()
            .HasMaxLength(Constants.ChatNameMaxLength);
    }
}
