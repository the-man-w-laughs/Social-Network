using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.Contracts.Medias;

public interface ICommunityMediaOwnerRepository : IRepository<CommunityMediaOwner> {
    Task<List<CommunityMediaOwner>> GetCommunityMediaOwnerList(uint communityId);
}