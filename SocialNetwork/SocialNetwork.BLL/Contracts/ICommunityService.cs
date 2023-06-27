using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.BLL.Contracts;

public interface ICommunityService
{
    Task<CommunityResponseDto> AddCommunity(CommunityRequestDto newCommunity);
    Task<CommunityMemberResponseDto> AddCommunityMember(uint communityId, uint userId, CommunityMemberType communityMemberType);    
    Task<List<CommunityResponseDto>> GetCommunities(int limit, int currCursor);
    Task<CommunityMember> GetCommunityOwner(uint communityId);
    Task<CommunityMember> GetCommunityMember(uint communityId, uint userId);
    Task<CommunityResponseDto> DeleteCommunity(uint userId, uint communityId);
    Task<bool> IsUserCommunityMember(uint communityId, uint userId);
    Task<CommunityPostResponseDto> AddCommunityPost(uint proposerId, uint communityId, PostRequestDto postRequestDto);
    Task<List<CommunityPostResponseDto>> GetCommunityPosts(uint userId, uint communityId, int limit, int currCursor);
    Task<CommunityResponseDto> ChangeCommunity(uint userId, uint communityId, CommunityPatchRequestDto newCommunity);
}