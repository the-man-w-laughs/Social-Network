using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork.DAL.Contracts.Medias;

public class UserMediaOwnerRepository : Repository<UserMediaOwner>, IUserMediaOwnerRepository
{
    public UserMediaOwnerRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) { }

    public async Task<UserMediaOwner> AddUserMediaOwner(uint userId, uint mediaId)
    {
        var newUserMediaOwner = new UserMediaOwner() { UserId = userId, MediaId = mediaId };
        await AddAsync(newUserMediaOwner);
        await SaveAsync();
        return newUserMediaOwner;
    }

    public async Task<List<UserMediaOwner>> GetUserMediaOwnerList(uint userId)
    {
        return await GetAllAsync((userMediaOwner) => userMediaOwner.UserId == userId);
    }
}