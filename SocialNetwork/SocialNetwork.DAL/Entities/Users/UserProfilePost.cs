using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.DAL.Entities.Users;

public partial class UserProfilePost
{
    public uint UserPostId { get; set; }

    public uint? PostId { get; set; }

    public uint? UserId { get; set; }

    public virtual Post? Post { get; set; }

    public virtual User? User { get; set; }
}
