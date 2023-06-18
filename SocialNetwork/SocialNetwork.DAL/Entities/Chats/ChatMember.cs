using SocialNetwork.DAL.Chats;
using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Chats;

public partial class ChatMember
{       
    public uint Id { get; set; }
    public ChatMemberType TypeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public uint UserId { get; set; }
    public uint ChatId { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Chat Chat { get; set; } = null!;
    public virtual ICollection<MessageLike> MessageLikes { get; set; } = new List<MessageLike>();
}
