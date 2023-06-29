namespace SocialNetwork.BLL.DTO.Messages.Request;

public class MessageRequestDto
{
    public string? Content { get; set; }
    public uint? RepliedMessageId { get; set; }
    public List<uint>? Attachments { get; set; }
}