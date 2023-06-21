using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Communities;
using SocialNetwork.DAL.Entities.Communities;

namespace SocialNetwork.DAL.Repositories.Communities;

public class CommunityRepository : Repository<Community>, ICommunityRepository
{
    public CommunityRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}