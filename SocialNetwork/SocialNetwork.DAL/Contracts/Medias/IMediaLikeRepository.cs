using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.DAL.Contracts.Medias;

public interface IMediaLikeRepository : IRepository<MediaLike> {
    Task<MediaLike> LikeMedia(uint userId, uint mediaId);
    Task<List<MediaLike>> GetMediaLikes(uint mediaId);
    Task<MediaLike?> UnLikeMedia(uint userId, uint mediaId);
}