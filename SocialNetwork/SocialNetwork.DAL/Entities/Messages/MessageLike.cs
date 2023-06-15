using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.DAL.Entities.Messages;

public partial class MessageLike
{
    public int Id { get; set; }

    public uint ChatMemberId { get; set; }
    public uint MessageId { get; set; }

    public virtual ChatMember ChatMember { get; set; } = null!;
    public virtual Message Message { get; set; } = null!;
}
