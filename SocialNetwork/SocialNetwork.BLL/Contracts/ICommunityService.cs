using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.BLL.Contracts;

public interface ICommunityService
{
    Task<Community> AddCommunity(Community newCommunity);
    Task<CommunityMember> AddCommunityMember(CommunityMember communityMember);
    Task<List<Community>> GetCommunities(int limit, int currCursor);
    Task<Community?> GetCommunityById(uint communityId);
    Task<CommunityMember> GetCommunityOwner(uint communityId);
    Task<Community> DeleteCommunity(uint communityId);
    Task<bool> IsUserCommunityMember(uint communityId, uint userId);
    Task<CommunityPost> AddCommunityPost(uint communityId, Post post, uint proposerId);
    Task<List<CommunityPost>> GetCommunityPosts(uint communityId, int limit, int currCursor);
    Task<CommunityResponseDto> ChangeCommunity(uint communityId, CommunityPatchRequestDto newCommunity);
    Task<List<CommunityMember>> GetCommunityMember(uint communityId, uint communityMemberTypeId);
}