using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.BLL.Contracts;

public interface ICommunityService
{
    Task<Community> AddCommunity(Community newCommunity);
    Task<CommunityMember> AddCommunityMember(CommunityMember communityMember);
    Task<List<Community>> GetCommunities(int limit, int currCursor);
    Task<Community?> GetCommunityById(uint communityId);
    Task<CommunityMember> GetCommunityOwner(uint communityId);
    Task<Community> DeleteCommunity(uint communityId);
}