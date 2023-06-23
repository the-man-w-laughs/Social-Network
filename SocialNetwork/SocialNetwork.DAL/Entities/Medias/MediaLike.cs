using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Medias;

public partial class MediaLike
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public uint UserId { get; set; }
    public uint MediaId { get; set; }    

    public virtual User User { get; set; } = null!;
    public virtual Media Media { get; set; } = null!;
}
