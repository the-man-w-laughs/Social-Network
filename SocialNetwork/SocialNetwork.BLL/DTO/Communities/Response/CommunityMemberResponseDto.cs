
using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.BLL.DTO.Communities.Response;

public class CommunityMemberResponseDto
{
    public uint Id { get; set; }
    public CommunityMemberType TypeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public uint UserId { get; set; }
    public uint CommunityId { get; set; }
}