namespace SocialNetwork.BLL.DTO.Messages.Response;

public class MediaLikeResponseDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public uint UserId { get; set; }
    public uint MediaId { get; set; }
}