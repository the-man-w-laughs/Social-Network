namespace SocialNetwork.DAL.Entities.Communities;

public class Community
{
    public int CommunityId { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public bool IsPrivate { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}