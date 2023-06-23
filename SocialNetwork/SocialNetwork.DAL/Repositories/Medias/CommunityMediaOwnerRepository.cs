using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork.DAL.Contracts.Medias;

public class CommunityMediaOwnerRepository : Repository<CommunityMediaOwner>, ICommunityMediaOwnerRepository
{
    public CommunityMediaOwnerRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) { }

    public Task<List<CommunityMediaOwner>> GetCommunityMediaOwnerList(uint communityId)
    {
        throw new NotImplementedException();
    }
}