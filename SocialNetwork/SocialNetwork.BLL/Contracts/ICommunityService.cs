using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.BLL.Contracts;

public interface ICommunityService
{
    Task<CommunityResponseDto> AddCommunity(CommunityRequestDto newCommunity);
    Task<CommunityResponseDto> GetCommunity(uint userId, uint communityId);
    Task<List<CommunityResponseDto>> GetCommunities(int limit, int currCursor);        
    Task<CommunityResponseDto> DeleteCommunity(uint userId, uint communityId);    
    Task<CommunityPostResponseDto> AddCommunityPost(uint proposerId, uint communityId, PostRequestDto postRequestDto);
    Task<List<CommunityPostResponseDto>> GetCommunityPosts(uint userId, uint communityId, int limit, int currCursor);
    Task<CommunityResponseDto> ChangeCommunity(uint userId, uint communityId, CommunityPatchRequestDto newCommunity);
    Task<CommunityMemberResponseDto> AddCommunityOwner(uint id, uint userId);
    Task<CommunityMemberResponseDto> AddCommunityMember(uint userId, uint communityId, uint userIdToAdd);
    Task<List<CommunityMemberResponseDto>> GetCommunityMembers(uint userId, uint communityId, uint? communityMemberTypeId, int limit, int currCursor);
    Task<CommunityMemberResponseDto> ChangeCommunityMember(uint userId, uint communityId, uint userIdToChange, CommunityMemberRequestDto communityMemberRequestDto);
    Task<CommunityMemberResponseDto> DeleteCommunityMember(uint userId, uint communityId, uint userIdToAdd);
}