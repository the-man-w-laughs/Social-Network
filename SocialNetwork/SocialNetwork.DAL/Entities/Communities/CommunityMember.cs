using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Entities.Communities;

public partial class CommunityMember
{
    public uint CommunityMemberId { get; set; }

    public uint? UserId { get; set; }

    public uint? CommunityId { get; set; }

    public byte? CommunityMemberTypeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Community? Community { get; set; }

    public virtual CommunityMemberType? CommunityMemberType { get; set; }

    public virtual User? User { get; set; }
}
