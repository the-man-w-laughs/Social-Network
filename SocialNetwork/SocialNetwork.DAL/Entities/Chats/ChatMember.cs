using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Chats;

public partial class ChatMember
{
    public uint UserChatId { get; set; }

    public uint UserId { get; set; }

    public uint ChatId { get; set; }

    public byte ChatMemberType { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Chat Chat { get; set; } = null!;

    public virtual ChatMembersType ChatMemberTypeNavigation { get; set; } = null!;

    public virtual ICollection<MessageLike> MessageLikes { get; set; } = new List<MessageLike>();

    public virtual User User { get; set; } = null!;
}
