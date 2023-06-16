using SocialNetwork.DAL.Entities.Enums;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Communities;

public partial class CommunityMember
{      
    public uint Id { get; set; }
    public CommunityMemberType TypeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public uint UserId { get; set; }
    public uint CommunityId { get; set; }
    
    public virtual Community Community { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
