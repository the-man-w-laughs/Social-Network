using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.BLL.DTO.Messages.Request;

public class MessagePostDto
{
    public string? Content { get; set; }
    public uint? RepliedMessageId { get; set; }
    public List<uint>? Attachments { get; set; }    
}