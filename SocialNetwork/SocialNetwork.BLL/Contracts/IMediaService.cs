using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.BLL.Contracts;

public interface IMediaService
{
    Task<MediaResponseDto> AddUserMedia(string filePath, uint userId, string fileName);
    Task<Media> GetMedia(uint mediaId);
    Task<List<MediaResponseDto>?> GetUserMediaList(uint userId, int limit, int currCursor);
    Task<MediaResponseDto> AddCommunityMedia(string filePath, uint communityId, string fileName);  
    Task<List<MediaResponseDto>?> GetCommunityMediaList(uint communityId, int limit, int currCursor);
    Task<MediaResponseDto?> DeleteMedia(uint mediaId);
    Task<MediaLikeResponseDto> LikeMedia(uint userId, uint mediaId);
    Task<List<MediaLikeResponseDto>?> GetMediaLikes(uint mediaId, int limit, int currCursor);
    Task<MediaLikeResponseDto?> UnLikeMedia(uint userId, uint mediaId);
}