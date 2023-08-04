namespace SocialNetwork.BLL.DTO.Chats.Response;

public class ChatResponseDto
{
    public uint Id { get; set; }
    public string Name { get; set; } = null!;
    public int UserCount { get; set; }    
    public uint? ChatPictureId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }        
}