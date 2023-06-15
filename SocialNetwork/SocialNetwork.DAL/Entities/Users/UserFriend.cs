namespace SocialNetwork.DAL.Entities.Users;

public partial class UserFriend
{
    public uint UserFriendId { get; set; }

    public uint? User1Id { get; set; }

    public uint? User2Id { get; set; }

    public byte? FriendshipType { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual FriendshipType? FriendshipTypeNavigation { get; set; }

    public virtual User? User1 { get; set; }

    public virtual User? User2 { get; set; }
}
