namespace SocialNetwork.DAL.Entities.Users;

public sealed class UserFriend
{
    public enum FriendshipType { Default, BestFriend,  }
    
    public uint Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public FriendshipType FriendshipTypeId { get; set; }

    public uint User1Id { get; set; }
    public uint User2Id { get; set; }
    
    public User User1 { get; set; } = null!;
    public User User2 { get; set; } = null!;
}
