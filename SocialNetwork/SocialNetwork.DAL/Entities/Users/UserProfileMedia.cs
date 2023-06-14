namespace SocialNetwork.DAL.Entities.Users;

public class UserProfileMedia
{
    public int UserProfileMediaId { get; set; }
    
    public int UserId { get; set; }
    
    public int MediaId { get; set; }
}