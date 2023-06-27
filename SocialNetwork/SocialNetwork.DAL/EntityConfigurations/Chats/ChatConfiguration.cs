using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.DAL.EntityConfigurations.Chats;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("chats");
        
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        
        builder.HasIndex(e => e.Id, "chat_id_UNIQUE").IsUnique();

        builder.Property(e => e.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd(); ;

        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired()
            .HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at")
            .HasColumnType("datetime");
        builder.Property(e => e.Name).HasColumnName("name").IsRequired()
            .HasMaxLength(Constants.ChatNameMaxLength);
        builder.Property(e => e.ChatPictureId)
.HasColumnName("chat_picture_id");
        builder.HasOne(up => up.ChatPicture).WithMany(u => u.Chat)
            .HasForeignKey(up => up.ChatPictureId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_picture_chat_picture");
    }
}
