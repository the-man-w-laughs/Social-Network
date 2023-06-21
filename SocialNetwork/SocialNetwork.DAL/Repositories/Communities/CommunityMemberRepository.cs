using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Communities;
using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.DAL.Repositories.Communities;

public class CommunityMemberRepository : Repository<CommunityMember>, ICommunityMemberRepository
{
    public CommunityMemberRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}