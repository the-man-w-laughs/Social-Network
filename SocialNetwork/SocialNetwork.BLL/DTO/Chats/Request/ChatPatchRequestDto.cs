namespace SocialNetwork.BLL.DTO.Chats.Request;

public class ChatPatchRequestDto
{
    public uint? ChatPictureId { get; set; }
    public string? Name { get; set; } = null!;     
}