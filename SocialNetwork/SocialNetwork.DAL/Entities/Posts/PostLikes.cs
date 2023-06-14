namespace SocialNetwork.DAL.Entities.Posts;

public class PostLikes
{
    public int PostLikeId { get; set; }
    
    public int PostId { get; set; }
    
    public int UserId { get; set; }
}