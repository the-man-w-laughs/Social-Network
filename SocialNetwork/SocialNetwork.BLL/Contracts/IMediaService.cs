using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.BLL.Contracts;

public interface IMediaService
{
    public Task<MediaResponseDto> AddUserMedia(string filePath, uint userId, string fileName);
    public Task<Media> GetLocalMedia(uint mediaId);
    public Task<MediaResponseDto> GetMedia(uint mediaId);
    public Task<List<MediaResponseDto>> GetUserMediaList(uint userId, int limit, int currCursor);        
    public Task<MediaResponseDto> DeleteMedia(uint userId, uint mediaId);
    public Task<MediaLikeResponseDto> LikeMedia(uint userId, uint mediaId);    
    public Task<List<MediaLikeResponseDto>> GetMediaLikes(uint mediaId, int limit, int currCursor);
    public Task<MediaLikeResponseDto> UnLikeMedia(uint userId, uint mediaId);
}