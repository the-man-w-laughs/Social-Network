namespace SocialNetwork.DAL.Entities.Posts;

public class Post
{
    public int PostId { get; set; }
    
    public string? Content { get; set; }
    
    public int RepliedPost { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}