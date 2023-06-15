namespace SocialNetwork.DAL.Entities.Users;

public partial class UserFollower
{
    public uint Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public uint TargetId { get; set; }
    public uint SourceId { get; set; }
    
    public virtual User Source { get; set; } = null!;
    public virtual User Target { get; set; } = null!;
}
