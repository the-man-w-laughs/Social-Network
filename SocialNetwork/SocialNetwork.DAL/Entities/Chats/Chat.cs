using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.Entities.Chats;

public partial class Chat
{
    public uint ChatId { get; set; }

    public string? Name { get; set; }

    public string CreatedAt { get; set; } = null!;

    public virtual ICollection<ChatMember> ChatMembers { get; set; } = new List<ChatMember>();

    public virtual ICollection<MessageMedia> MessageMedia { get; set; } = new List<MessageMedia>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
