namespace SocialNetwork.DAL.Entities.Chats;

public class MessageMedia
{
    public int MessageMediaId { get; set; }
    
    public int MessageId { get; set; }
    
    public int MediaId { get; set; }
    
    public int ChatId { get; set; }
}