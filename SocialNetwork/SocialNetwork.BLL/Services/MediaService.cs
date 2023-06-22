using SocialNetwork.BLL.Contracts;
using SocialNetwork.DAL.Contracts;
using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.BLL.Services
{
    internal class MediaService : IMediaService
    {
        private readonly IMediaRepository _mediaRepository;

        public MediaService(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public Task<Media> DeleteMedia(uint mediaId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Media>> GetCommunityMediaList(uint communityId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Media>> GetUserMediaList(uint userId)
        {
            throw new NotImplementedException();
        }

        public Task<MediaLike> LikeMedia(uint userId, uint mediaId)
        {
            throw new NotImplementedException();
        }

        public Task<Media> SaveCommunityMedia(string webRootPath, uint communityId, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<Media> SaveUserMedia(string webRootPath, uint userId, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<MediaLike> UnLikeMedia(uint userId, uint mediaId)
        {
            throw new NotImplementedException();
        }
    }    
}
