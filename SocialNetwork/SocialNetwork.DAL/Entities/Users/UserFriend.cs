namespace SocialNetwork.DAL.Entities.Users;

public class UserFriend
{
    public int UserFriendId { get; set; }
    
    public int UserId { get; set; }
    
    public int FriendId { get; set; }
    
    public byte FriendshipTypeId { get; set; }
    
    public DateTime CreatedAt { get; set; }
}