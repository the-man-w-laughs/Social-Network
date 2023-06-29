using AutoMapper;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.BLL.Services
{
    internal class MediaService : IMediaService
    {
        private readonly IMapper _mapper;
        private readonly IMediaRepository _mediaRepository;
        private readonly IMediaLikeRepository _mediaLikeRepository;                

        public MediaService(
            IMapper mapper,
            IMediaRepository mediaRepository,
            IMediaLikeRepository mediaLikeRepository)
        {
            _mapper = mapper;
            _mediaRepository = mediaRepository;
            _mediaLikeRepository = mediaLikeRepository;                        
        }
        public async Task<MediaResponseDto> AddUserMedia(string filePath, uint userId, string fileName)
        {
            var newMedia = await _mediaRepository.AddMedia(userId, filePath, fileName);            
            return _mapper.Map<MediaResponseDto>(newMedia);
        }
        public async Task<MediaResponseDto> GetMedia(uint mediaId)
        {
            var media = await GetLocalMedia(mediaId);
            var mediaResponseDto = _mapper.Map<MediaResponseDto>(media);

            mediaResponseDto.LikeCount = (await _mediaLikeRepository.GetMediaLikes(mediaId)).Count;            
            return mediaResponseDto;
        }

        public async Task<Media> GetLocalMedia(uint mediaId)
        {
            var media = await _mediaRepository.GetByIdAsync(mediaId) ??
                throw new NotFoundException($"Media (ID: {mediaId}) if not found."); ;            
            return media;
        }
        public async Task<List<MediaResponseDto>> GetUserMediaList(uint userId, int limit, int currCursor)
        {
            var mediaList = await _mediaRepository.GetAllAsync(m => m.OwnerId == userId);
            var paginatedMediaList = mediaList.OrderBy(cm => cm.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
            return _mapper.Map<List<MediaResponseDto>>(paginatedMediaList);
        }

        public async Task<bool> IsUserMediaOwner(uint userId, uint mediaId)
        {
            var result = await _mediaRepository.GetAsync(e => e.OwnerId == userId);
            if (result != null) return true; else return false;
        }

        public async Task<MediaResponseDto> DeleteMedia(uint userId, uint mediaId)
        {
            var media = await GetLocalMedia(mediaId);
            if (media.OwnerId != userId)
                throw new OwnershipException($"User (ID: {userId}) is not owner of media (ID: {mediaId})");

            if (!System.IO.File.Exists(media.FilePath))
                throw new NotFoundException($"There's no file of this media (ID: {mediaId}).");
            System.IO.File.Delete(media.FilePath);

            _mediaRepository.Delete(media);
            await _mediaRepository.SaveAsync();
            return _mapper.Map<MediaResponseDto>(media);
        }
        public async Task<MediaLike?> GetMediaLike(uint userId, uint mediaId)
        {
            var result = await _mediaLikeRepository.GetAsync((mediaLike) => mediaLike.UserId == userId && mediaLike.MediaId == mediaId);
            return result;
        }

        public async Task<MediaLikeResponseDto> LikeMedia(uint userId, uint mediaId)
        {
            await GetLocalMedia(mediaId);

            if (await GetMediaLike(userId, mediaId) != null) throw new DuplicateEntryException("User already liked this media.");
            var newLike = await _mediaLikeRepository.LikeMedia(userId, mediaId);
            return _mapper.Map<MediaLikeResponseDto>(newLike);
        }


        public async Task<List<MediaLikeResponseDto>> GetMediaLikes(uint mediaId, int limit, int currCursor)
        {
            await GetLocalMedia(mediaId);

            var mediaLikesList = await _mediaLikeRepository.GetMediaLikes(mediaId);
            var paginatedmediaLikesList = mediaLikesList.OrderBy(cm => cm.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
            return _mapper.Map<List<MediaLikeResponseDto>>(paginatedmediaLikesList);
        }

        public async Task<MediaLikeResponseDto> UnLikeMedia(uint userId, uint mediaId)
        {
            await GetLocalMedia(mediaId);

            var mediaLike = await GetMediaLike(userId, mediaId);
            if (mediaLike == null) throw new NotFoundException("User didn't like this media.");
            _mediaLikeRepository.Delete(mediaLike);
            await _mediaLikeRepository.SaveAsync();
            return _mapper.Map<MediaLikeResponseDto>(mediaLike);
        }
    }
}
