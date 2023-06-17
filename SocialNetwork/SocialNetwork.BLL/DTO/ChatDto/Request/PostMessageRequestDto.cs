namespace SocialNetwork.BLL.DTO.ChatDto.Request;

public class PostMessageRequestDto
{
    public string? Content { get; set; }
    public uint? RepliedMessageId { get; set; }
}