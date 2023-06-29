using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.BLL.DTO.Comments.Response;

public class CommentResponseDto
{
    public uint Id { get; set; }
    public uint AuthorId { get; set; }
    public uint PostId { get; set; }
    public uint? RepliedComment { get; set; }
    public string? Content { get; set; }
    public ICollection<MediaResponseDto>? Attachments { get; set; } = new List<MediaResponseDto>();
    public uint? LikeCount { get; set; }    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }        
}