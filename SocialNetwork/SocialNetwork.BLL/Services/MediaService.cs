using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Contracts.Messages;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Entities.Users;
using System.Collections.Generic;

namespace SocialNetwork.BLL.Services
{
    internal class MediaService : IMediaService
    {
        private readonly IMapper _mapper;
        private readonly IMediaRepository _mediaRepository;
        private readonly IMediaLikeRepository _mediaLikeRepository;
        private readonly ICommunityMediaOwnerRepository _communityMediaOwnerRepository;
        private readonly IUserMediaOwnerRepository _userMediaOwnerRepository;

        public MediaService(
            IMapper mapper,
            IMediaRepository mediaRepository,
            IMediaLikeRepository mediaLikeRepository,
            IUserMediaOwnerRepository userMediaOwnerRepository,
            ICommunityMediaOwnerRepository communityMediaOwnerRepository)
        {
            _mapper = mapper;
            _mediaRepository = mediaRepository;
            _mediaLikeRepository = mediaLikeRepository;
            _communityMediaOwnerRepository = communityMediaOwnerRepository;
            _userMediaOwnerRepository = userMediaOwnerRepository;
        }
        public async Task<MediaResponseDto> AddUserMedia(string filePath, uint userId, string fileName)
        {
            var newMedia = await _mediaRepository.AddMedia(filePath, OwnerType.User, fileName);
            await _userMediaOwnerRepository.AddUserMediaOwner(userId, newMedia.Id);
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
            var media = await _mediaRepository.GetByIdAsync(mediaId) ?? throw new NotFoundException("No media with this id."); ;            
            return media;
        }
        public async Task<List<MediaResponseDto>> GetUserMediaList(uint userId, int limit, int currCursor)
        {
            var userMediaOwners = await _userMediaOwnerRepository.GetUserMediaOwnerList(userId);
            var mediaIds = userMediaOwners.Select(umo => umo.MediaId).ToList();

            var mediaList = await _mediaRepository.GetAllAsync(media => mediaIds.Contains(media.Id));
            var paginatedMediaList = mediaList.OrderBy(cm => cm.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
            return _mapper.Map<List<MediaResponseDto>>(paginatedMediaList);
        }
        public async Task<MediaResponseDto> AddCommunityMedia(string filePath, uint communityId, string fileName)
        {
            var newMedia = await _mediaRepository.AddMedia(filePath, OwnerType.User, fileName);            
            await _communityMediaOwnerRepository.AddCommunityMediaOwner(communityId, newMedia.Id);
            return _mapper.Map<MediaResponseDto>(newMedia);
        }

        public async Task<List<MediaResponseDto>> GetCommunityMediaList(uint communityId, int limit, int currCursor)
        {
            var communityMediaOwners = await _communityMediaOwnerRepository.GetCommunityMediaOwnerList(communityId);
            var mediaIds = communityMediaOwners.Select(umo => umo.MediaId).ToList();

            var mediaList = await _mediaRepository.GetAllAsync(media => mediaIds.Contains(media.Id));
            var paginatedMediaList = mediaList.OrderBy(cm => cm.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
            return _mapper.Map<List<MediaResponseDto>>(paginatedMediaList);
        }

        public async Task<bool> IsUserMediaOwner(uint userId, uint mediaId)
        {
            var result = await _userMediaOwnerRepository.GetAsync(e => e.UserId == userId && e.MediaId == mediaId);
            if (result != null) return true; else return false;
        }

        public async Task<Media> DeleteMedia(uint userId, uint mediaId)
        {
            if (! await IsUserMediaOwner(userId, mediaId)) throw new OwnershipException("User is not owner of this media.");
            return await DeleteMedia(mediaId);
        }
        public async Task<Media> DeleteMedia(uint mediaId)
        {
            var media = await _mediaRepository.DeleteById(mediaId);
            if (media != null)
            {
                await _mediaLikeRepository.SaveAsync();
                return media;
            }
            else
            {
                throw new NotFoundException("Media is not found.");
            }
        }

        public async Task<MediaLikeResponseDto> LikeMedia(uint userId, uint mediaId)
        {            
            if (await IsUserLiked(userId, mediaId)) throw new DuplicateEntryException("User already liked this media.");
            var newLike = await _mediaLikeRepository.LikeMedia(userId, mediaId);
            return _mapper.Map<MediaLikeResponseDto>(newLike);
        }

        public async Task<bool> IsUserLiked(uint userId, uint mediaId)
        {
            var result = await _mediaLikeRepository.GetAsync((mediaLike) => mediaLike.UserId == userId && mediaLike.MediaId == mediaId);
            if (result != null) return true; else return false;                
        }

        public async Task<List<MediaLikeResponseDto>> GetMediaLikes(uint mediaId, int limit, int currCursor)
        {
            var mediaLikesList = await _mediaLikeRepository.GetMediaLikes(mediaId);
            var paginatedmediaLikesList = mediaLikesList.OrderBy(cm => cm.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
            return _mapper.Map<List<MediaLikeResponseDto>>(paginatedmediaLikesList);
        }

        public async Task<MediaLikeResponseDto> UnLikeMedia(uint userId, uint mediaId)
        {            
            if (!await IsUserLiked(userId, mediaId)) throw new NotFoundException("User didn't like this media.");
            var mediaLike = await _mediaLikeRepository.UnLikeMedia(userId, mediaId);
            return _mapper.Map<MediaLikeResponseDto>(mediaLike);
        }
    }
}
