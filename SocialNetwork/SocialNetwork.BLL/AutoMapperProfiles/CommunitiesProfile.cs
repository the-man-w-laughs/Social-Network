using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.BLL.AutoMapperProfiles;

public class CommunitiesProfile : BaseProfile
{
    public CommunitiesProfile()
    {
        CreateMap<CommunityRequestDto, Community>();
        
        CreateMap<CommunityMember, CommunityMemberResponseDto>();
        CreateMap<Community, CommunityResponseDto>().ForMember(
            dto => dto.UserCount, 
            expression => expression.MapFrom(community => community.CommunityMembers.Count));
    }
}