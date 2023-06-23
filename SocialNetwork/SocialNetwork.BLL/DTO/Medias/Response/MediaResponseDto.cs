using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.BLL.DTO.Medias.Response;

public class MediaResponseDto
{
    public uint Id { get; set; }
    public string FileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public int? LikeCount { get; set; }
    public MediaType MediaTypeId { get; set; }
    public OwnerType OwnerTypeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}