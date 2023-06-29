using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.BLL.DTO.Medias.Response;

public class MediaResponseDto
{
    public uint Id { get; set; }
    public string FileName { get; set; } = null!;    
    public int? LikeCount { get; set; }
    public uint OwnerId { get; set; }
    public MediaType MediaTypeId { get; set; }    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}