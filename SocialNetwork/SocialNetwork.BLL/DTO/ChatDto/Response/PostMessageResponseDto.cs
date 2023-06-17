namespace SocialNetwork.BLL.DTO.ChatDto.Response;

public class PostMessageResponseDto
{
    public uint Id { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public uint ChatId { get; set; }
    public uint SenderId { get; set; }
    public uint? RepliedMessageId { get; set; }
}