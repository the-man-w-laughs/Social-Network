namespace SocialNetwork.DAL.Entities.Users;

public class UserFollower
{
    public int UserFollowerId { get; set; }
    
    public int UserId { get; set; }
    
    public int FollowerId { get; set; }
    
    public DateTime CreatedAt { get; set; }
}