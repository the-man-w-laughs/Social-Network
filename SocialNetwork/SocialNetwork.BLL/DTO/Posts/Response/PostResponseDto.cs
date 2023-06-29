using SocialNetwork.BLL.DTO.Medias.Response;

namespace SocialNetwork.BLL.DTO.Posts.Response;

public class PostResponseDto
{
    public uint Id { get; set; }
    public string? Content { get; set; }
    public int? LikeCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<MediaResponseDto> Attachments { get; set; } = new List<MediaResponseDto>();
    public uint? RepostId { get; set; }
}