namespace SocialNetwork.DAL.Entities.Communities;

public partial class CommunityMemberType
{
    public byte CommunityMemberTypeId { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<CommunityMember> CommunityMembers { get; set; } = new List<CommunityMember>();
}
