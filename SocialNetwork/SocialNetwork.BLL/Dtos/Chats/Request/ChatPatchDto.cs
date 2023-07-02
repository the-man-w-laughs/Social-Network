namespace SocialNetwork.BLL.DTO.Chats.Request;

public class ChatPatchDto
{
    public uint? ChatPictureId { get; set; }
    public string? Name { get; set; } = null!;     
}