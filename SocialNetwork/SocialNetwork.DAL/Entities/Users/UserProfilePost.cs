using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.DAL.Entities.Users;

public sealed class UserProfilePost
{
    public uint Id { get; set; }

    public uint PostId { get; set; }
    public uint UserId { get; set; }

    public Post Post { get; set; } = null!;
    public User User { get; set; } = null!;
}
