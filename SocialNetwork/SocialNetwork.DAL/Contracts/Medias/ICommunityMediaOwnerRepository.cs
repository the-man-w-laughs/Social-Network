using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.Contracts.Medias;

public interface ICommunityMediaOwnerRepository : IRepository<CommunityMediaOwner> {
    Task<CommunityMediaOwner> AddCommunityMediaOwner(uint communityId, uint mediaId);    
    Task<List<CommunityMediaOwner>> GetCommunityMediaOwnerList(uint communityId);
}