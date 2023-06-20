using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.Entities.Chats;

public partial class Chat
{
    public uint Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ChatMember> ChatMembers { get; set; } = new List<ChatMember>();
    public virtual ICollection<MessageMedia> ChatMedias { get; set; } = new List<MessageMedia>();
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
