using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.DAL.Entities.Messages;

public partial class Message
{
    public uint Id { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public uint ChatId { get; set; }
    public uint SenderId { get; set; }
    public uint? RepliedMessageId { get; set; }
    
    public virtual Chat Chat { get; set; } = null!;
    public virtual Message? RepliedMessage { get; set; }
    
    public virtual ICollection<Message> InverseRepliedMessage { get; set; } = new List<Message>();
    public virtual ICollection<MessageLike> MessageLikes { get; set; } = new List<MessageLike>();
    public virtual ICollection<MessageMedia> MessageMedia { get; set; } = new List<MessageMedia>();
}
