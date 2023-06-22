using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.BLL.DTO.Messages.Response;

public class MediaResponseDto
{
    public uint Id { get; set; }
    public string FileName { get; set; } = null!;    
    public MediaType MediaTypeId { get; set; }
    public OwnerType OwnerTypeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}