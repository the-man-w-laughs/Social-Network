using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Posts;

public partial class PostLike
{
    public int PostLikeId { get; set; }

    public uint PostId { get; set; }

    public uint UserId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
