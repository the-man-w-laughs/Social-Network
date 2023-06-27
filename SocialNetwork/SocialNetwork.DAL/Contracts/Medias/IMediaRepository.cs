using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.DAL.Contracts.Medias;

public interface IMediaRepository : IRepository<Media> {
    Task<Media> AddMedia(uint userId, string filePath, string fileName);     
}