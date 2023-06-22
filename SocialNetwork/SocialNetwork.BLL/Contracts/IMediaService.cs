using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.BLL.Contracts;

public interface IMediaService
{
    Task<Media> SaveUserMedia(string webRootPath, uint userId, string fileName);  
    Task<List<Media>> GetUserMediaList(uint userId);
    Task<Media> SaveCommunityMedia(string webRootPath, uint communityId, string fileName);  
    Task<List<Media>> GetCommunityMediaList(uint communityId);

    Task<Media> DeleteMedia(uint mediaId);

    Task<MediaLike> LikeMedia(uint userId, uint mediaId);
    Task<MediaLike> UnLikeMedia(uint userId, uint mediaId);
}