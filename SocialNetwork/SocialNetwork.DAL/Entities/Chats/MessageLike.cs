namespace SocialNetwork.DAL.Entities.Chats;

public class MessageLike
{
    public int MessageLikeId { get; set; }
    
    public int ChatMemberId { get; set; }
    
    public int MessageId { get; set; }
}