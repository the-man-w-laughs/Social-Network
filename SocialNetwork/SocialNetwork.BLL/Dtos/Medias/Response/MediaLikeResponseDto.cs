namespace SocialNetwork.BLL.DTO.Medias.Response;

public class MediaLikeResponseDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public uint UserId { get; set; }
    public uint MediaId { get; set; }
}