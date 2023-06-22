using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.Contracts.Medias;

public interface IUserMediaOwnerRepository : IRepository<UserMediaOwner> {
    Task<UserMediaOwner> AddUserMediaOwner(uint userId, uint mediaId);
    Task<List<UserMediaOwner>> GetUserMediaOwnerList(uint userId);
}