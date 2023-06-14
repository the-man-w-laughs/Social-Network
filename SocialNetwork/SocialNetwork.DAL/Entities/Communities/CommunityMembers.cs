namespace SocialNetwork.DAL.Entities.Communities;

public class CommunityMembers
{
    public int CommunityMemberId { get; set; }
    
    public int UserId { get; set; }
    
    public int CommunityId { get; set; }
    
    public byte CommunityMemberTypeId { get; set; }
    
    public DateTime CreatedAt { get; set; }
}