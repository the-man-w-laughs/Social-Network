using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.DAL.Entities.Users;

public sealed class UserProfileMedia
{
    public uint Id { get; set; }

    public uint UserProfileId { get; set; }
    public uint MediaId { get; set; }

    public Media Media { get; set; } = null!;
    public UserProfile UserProfile { get; set; } = null!;
}
