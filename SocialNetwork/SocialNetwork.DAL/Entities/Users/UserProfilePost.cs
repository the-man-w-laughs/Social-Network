using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.DAL.Entities.Users;

public partial class UserProfilePost
{
    public uint Id { get; set; }

    public uint PostId { get; set; }
    public uint UserId { get; set; }

    public virtual Post Post { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
