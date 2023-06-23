using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.BLL.Contracts;

public interface IMediaService
{
    public Task<MediaResponseDto> AddUserMedia(string filePath, uint userId, string fileName);
    public Task<Media?> GetLocalMedia(uint mediaId);
    public Task<MediaResponseDto> GetMedia(uint mediaId);
    public Task<List<MediaResponseDto>?> GetUserMediaList(uint userId, int limit, int currCursor);
    public Task<MediaResponseDto> AddCommunityMedia(string filePath, uint communityId, string fileName);
    public Task<List<MediaResponseDto>?> GetCommunityMediaList(uint communityId, int limit, int currCursor);
    public Task<Media?> DeleteMedia(uint mediaId);
    public Task<MediaLikeResponseDto> LikeMedia(uint userId, uint mediaId);
    public Task<bool> IsUserLiked(uint userId, uint mediaId);
    public Task<List<MediaLikeResponseDto>?> GetMediaLikes(uint mediaId, int limit, int currCursor);
    public Task<MediaLikeResponseDto?> UnLikeMedia(uint userId, uint mediaId);
}