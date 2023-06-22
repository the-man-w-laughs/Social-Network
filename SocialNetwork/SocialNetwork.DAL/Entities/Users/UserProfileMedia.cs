using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.DAL.Entities.Users;

public partial class UserProfileMedia
{
    public uint Id { get; set; }

    public uint UserProfileId { get; set; }
    public uint MediaId { get; set; }

    public virtual Media Media { get; set; } = null!;
    public virtual UserProfile UserProfile { get; set; } = null!;
}
