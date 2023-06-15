namespace SocialNetwork.DAL.Entities.Users;

public partial class FriendshipType
{
    public byte FriendshipTypeId { get; set; }

    public string? FriendshipType1 { get; set; }

    public virtual ICollection<UserFriend> UserFriends { get; set; } = new List<UserFriend>();
}
