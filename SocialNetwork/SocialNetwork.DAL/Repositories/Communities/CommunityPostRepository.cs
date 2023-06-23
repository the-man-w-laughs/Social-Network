using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Communities;
using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.DAL.Repositories.Communities;

public class CommunityPostRepository : Repository<CommunityPost>, ICommunityPostRepository
{
    public CommunityPostRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}