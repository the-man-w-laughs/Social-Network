using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork.DAL.Contracts.Medias;

public class CommunityMediaOwnerRepository : Repository<CommunityMediaOwner>, ICommunityMediaOwnerRepository
{
    public CommunityMediaOwnerRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) { }

    public async Task<CommunityMediaOwner> AddCommunityMediaOwner(uint communityId, uint mediaId)
    {
        var newUserMediaOwner = new CommunityMediaOwner() { CommunityId = communityId, MediaId = mediaId };
        await AddAsync(newUserMediaOwner);
        await SaveAsync();
        return newUserMediaOwner;
    }
    public async Task<List<CommunityMediaOwner>> GetCommunityMediaOwnerList(uint communityId)
    {
        return await GetAllAsync((userMediaOwner) => userMediaOwner.CommunityId == communityId);
    }
}