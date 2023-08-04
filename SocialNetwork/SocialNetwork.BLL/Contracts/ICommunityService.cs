using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;

namespace SocialNetwork.BLL.Contracts;

public interface ICommunityService
{
    Task<CommunityResponseDto> AddCommunity(CommunityPostDto newCommunity);
    Task<CommunityResponseDto> GetCommunity(uint userId, uint communityId);
    Task<List<CommunityResponseDto>> GetCommunities(int limit, int currCursor);        
    Task<CommunityResponseDto> DeleteCommunity(uint userId, uint communityId);
    Task<CommunityResponseDto> DeleteCommunity(uint communityId);    
    Task<List<PostResponseDto>> GetCommunityPosts(uint userId, uint communityId, int limit, int currCursor);
    Task<CommunityResponseDto> ChangeCommunity(uint userId, uint communityId, CommunityPatchDto newCommunity);
    Task<CommunityMemberResponseDto> AddCommunityOwner(uint id, uint userId);
    Task<CommunityMemberResponseDto> AddCommunityMember(uint userId, uint communityId, uint userIdToAdd);
    Task<List<CommunityMemberResponseDto>> GetCommunityMembers(uint userId, uint communityId, uint? communityMemberTypeId, int limit, int currCursor);
    Task<CommunityMemberResponseDto> ChangeCommunityMember(uint userId, uint communityId, uint userIdToChange, CommunityMemberPutDto communityMemberRequestDto);
    Task<CommunityMemberResponseDto> DeleteCommunityMember(uint userId, uint communityId, uint userIdToAdd);
}