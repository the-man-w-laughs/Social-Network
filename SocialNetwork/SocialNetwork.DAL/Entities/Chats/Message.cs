namespace SocialNetwork.DAL.Entities.Chats;

public class Message
{
    public int MessageId { get; set; }
    
    public int GroupId { get; set; }
    
    public string Content { get; set; }
    
    public int RepliedMessage { get; set; }
    
    public int SenderId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}