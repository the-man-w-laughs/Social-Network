namespace SocialNetwork.BLL.DTO.Messages.Response;

public class MessageLikeResponseDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public uint ChatMemberId { get; set; }
    public uint MessageId { get; set; }
}