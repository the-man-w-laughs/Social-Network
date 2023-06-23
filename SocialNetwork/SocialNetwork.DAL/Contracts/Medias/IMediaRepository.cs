using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.DAL.Contracts.Medias;

public interface IMediaRepository : IRepository<Media> {
    Task<Media> AddMedia(string filePath, OwnerType ownerType, string fileName);     
}