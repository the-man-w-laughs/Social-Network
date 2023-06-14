namespace SocialNetwork.DAL.Entities.Chats;

public class ChatMember
{
    public int UserMemberId { get; set; }
    
    public int UserId { get; set; }
    
    public int ChatId { get; set; }
    
    public byte ChatMemberTypeId { get; set; }
    
    public DateTime CreatedAt { get; set; }
}