namespace SocialNetwork.DAL.Entities.Users;

public partial class UserFollower
{
    public uint UserFollowerId { get; set; }

    public uint? TargetId { get; set; }

    public uint? SourceId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User? Source { get; set; }

    public virtual User? Target { get; set; }
}
