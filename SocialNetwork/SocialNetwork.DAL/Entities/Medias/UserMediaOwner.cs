using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Medias;

public partial class UserMediaOwner
{
    public int Id { get; set; }   
    public uint MediaId { get; set; }
    public uint UserId { get; set; }    

    public virtual Media Media { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
