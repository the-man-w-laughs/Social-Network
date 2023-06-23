using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Medias;

public partial class CommunityMediaOwner
{
    public int Id { get; set; }   
    public uint MediaId { get; set; }
    public uint CommunityId { get; set; }    

    public virtual Media Media { get; set; } = null!;
    public virtual Community Community { get; set; } = null!;
}
