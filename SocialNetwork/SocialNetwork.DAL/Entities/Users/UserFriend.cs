namespace SocialNetwork.DAL.Entities.Users;

public partial class UserFriend
{
    public enum FriendshipType { Default, BestFriend,  }
    
    public uint Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public FriendshipType FriendshipTypeId { get; set; }

    public uint User1Id { get; set; }
    public uint User2Id { get; set; }
    
    public virtual User User1 { get; set; } = null!;
    public virtual User User2 { get; set; } = null!;
}
