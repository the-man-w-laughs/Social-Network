namespace SocialNetwork.BLL.DTO.Messages.Request;

public class MessagePatchDto
{
    public string? Content { get; set; }    
    public List<uint>? Attachments { get; set; }
}