using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.BLL.DTO.Communities.Request;

public class CommunityMemberGetRequestDto
{
    public CommunityMemberType TypeId { get; set; }
}