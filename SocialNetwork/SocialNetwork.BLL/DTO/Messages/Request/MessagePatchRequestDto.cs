namespace SocialNetwork.BLL.DTO.Messages.Request;

public class MessagePatchRequestDto
{
    public string? Content { get; set; }    
    public List<uint>? Attachments { get; set; }
}