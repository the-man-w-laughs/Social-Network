using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.DAL.Entities.Messages;

public partial class MessageMedia
{
    public uint MessageMediaId { get; set; }

    public uint MessageId { get; set; }

    public uint MediaId { get; set; }

    public uint ChatId { get; set; }

    public virtual Chat Chat { get; set; } = null!;

    public virtual Media Media { get; set; } = null!;

    public virtual Message Message { get; set; } = null!;
}
