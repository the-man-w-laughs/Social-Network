namespace SocialNetwork.DAL.Entities.Communities;

public class CommunityPost
{
    public int CommunityPostId { get; set; }
    
    public int CommunityId { get; set; }
    
    public int PostId { get; set; }
    
    public int ProposerId { get; set; }
}