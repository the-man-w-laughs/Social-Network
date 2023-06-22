using AutoMapper;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Contracts.Messages;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Medias;
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
            var newUserMediaOwner = _userMediaOwnerRepository.AddUserMediaOwner(userId, newMedia.Id);
            return _mapper.Map<MediaResponseDto>(newMedia);
        }
        public async Task<Media> GetMedia(uint mediaId)
        {
            var media = await _mediaRepository.GetByIdAsync(mediaId);
            return media;
        }

        public async Task<List<MediaResponseDto>?> GetUserMediaList(uint userId, int limit, int currCursor)
        {
            var userMediaOwners = await (_userMediaOwnerRepository.GetAllAsync((UserMediaOwner) => UserMediaOwner.UserId == userId));
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
            await _mediaRepository.SaveAsync();
            var newUserMediaOwner = new CommunityMediaOwner() { CommunityId = communityId, MediaId = newMedia.Id };
            await _communityMediaOwnerRepository.AddAsync(newUserMediaOwner);
            await _communityMediaOwnerRepository.SaveAsync();
            return _mapper.Map<MediaResponseDto>(newMedia);
        }

        public async Task<List<MediaResponseDto>?> GetCommunityMediaList(uint communityId, int limit, int currCursor)
        {
            var communityMediaOwners = await (_communityMediaOwnerRepository.GetAllAsync((UserMediaOwner) => UserMediaOwner.CommunityId == communityId));
            var mediaIds = communityMediaOwners.Select(umo => umo.MediaId).ToList();

            var mediaList = await _mediaRepository.GetAllAsync(media => mediaIds.Contains(media.Id));
            var paginatedMediaList = mediaList.OrderBy(cm => cm.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
            return _mapper.Map<List<MediaResponseDto>>(paginatedMediaList);
        }

        public async Task<MediaResponseDto?> DeleteMedia(uint mediaId)
        {
            var media = await _mediaRepository.DeleteById(mediaId);
            await _mediaLikeRepository.SaveAsync();
            return _mapper.Map<MediaResponseDto>(media);
        }


        public async Task<MediaLikeResponseDto> LikeMedia(uint userId, uint mediaId)
        {
            var newLike = await _mediaLikeRepository.LikeMedia(userId, mediaId);
            return _mapper.Map<MediaLikeResponseDto>(newLike);
        }

        public async Task<List<MediaLikeResponseDto>?> GetMediaLikes(uint mediaId, int limit, int currCursor)
        {
            var mediaLikesList = await _mediaLikeRepository.GetMediaLikes(mediaId);
            var paginatedmediaLikesList = mediaLikesList.OrderBy(cm => cm.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
            return _mapper.Map<List<MediaLikeResponseDto>>(paginatedmediaLikesList);
        }

        public async Task<MediaLikeResponseDto?> UnLikeMedia(uint userId, uint mediaId)
        {
            var mediaLike = await _mediaLikeRepository.UnLikeMedia(userId, mediaId);
            return _mapper.Map<MediaLikeResponseDto>(mediaLike);
        }
    }
}
