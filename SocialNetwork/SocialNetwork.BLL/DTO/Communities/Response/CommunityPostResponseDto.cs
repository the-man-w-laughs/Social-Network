using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.BLL.DTO.Communities.Response;

public class CommunityPostResponseDto
{
    public uint Id { get; set; }
    public uint CommunityId { get; set; }
    public uint PostId { get; set; }
    public uint? ProposerId { get; set; }
    public virtual PostResponseDto Post { get; set; } = null!;
}