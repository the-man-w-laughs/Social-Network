using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.DAL.EntityConfigurations.Chats;

public class ChatMembersTypeConfiguration : IEntityTypeConfiguration<ChatMembersType>
{
    public void Configure(EntityTypeBuilder<ChatMembersType> builder)
    {
        builder.HasKey(e => e.ChatMembersTypeId).HasName("PRIMARY");

        builder.ToTable("chat_members_types");

        builder.Property(e => e.ChatMembersTypeId).HasColumnName("chat_members_type_id");
        builder.Property(e => e.ChatMembersTypeName)
            .HasMaxLength(45)
            .HasColumnName("chat_members_type_name");
    }
}
