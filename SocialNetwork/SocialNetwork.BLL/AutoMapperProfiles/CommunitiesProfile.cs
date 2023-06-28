using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.BLL.AutoMapper;

public class CommunitiesProfile : BaseProfile
{
    public CommunitiesProfile()
    {
        CreateMap<CommunityRequestDto, Community>();
        CreateMap<CommunityMember, CommunityMemberResponseDto>();

        CreateMap<CommunityPost, CommunityPostResponseDto>();
        CreateMap<Community, CommunityResponseDto>();
    }
}