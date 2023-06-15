using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.DAL.Entities.Posts;

public partial class PostMedia
{
    public uint Id { get; set; }

    public uint PostId { get; set; }
    public uint MediaId { get; set; }

    public virtual Media Media { get; set; } = null!;
    public virtual Post Post { get; set; } = null!;
}
