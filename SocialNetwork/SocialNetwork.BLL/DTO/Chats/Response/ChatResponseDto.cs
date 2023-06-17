namespace SocialNetwork.BLL.DTO.ChatDto.Response;

public class ChatResponseDto
{
    public uint Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}