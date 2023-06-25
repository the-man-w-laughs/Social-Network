using AutoMapper;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IMediaRepository _mediaRepository;

    public UserService(IMapper mapper, IUserRepository userRepository, IUserProfileRepository userProfileRepository, IMediaRepository mediaRepository)
    {
        _mapper = mapper;
        _mediaRepository = mediaRepository;
        _userRepository = userRepository;
        _userProfileRepository = userProfileRepository;
    }

    public async Task<List<User>> GetUsers(int limit, int currCursor)
    {
        var users = await _userRepository.GetAllAsync();
        return users.OrderBy(u => u.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
    }

    public async Task<List<Chat>> GetUserChats(uint userId, int limit, int currCursor)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        return user!.ChatMembers.Select(cm => cm.Chat)
            .OrderBy(cm => cm.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
    }

    public async Task<List<Community>> GetUserCommunities(uint userId, int limit, int nextCursor)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        return user!.CommunityMembers.Select(c => c.Community)
            .OrderBy(c => c.Id)
            .Skip(nextCursor)
            .Take(limit)
            .ToList();
    }
    public async Task<List<Community>> GetUserManagedCommunities(uint userId, int limit, int nextCursor)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        return user!.CommunityMembers
            .Where(c => c.TypeId == CommunityMemberType.Admin || c.TypeId == CommunityMemberType.Owner)
            .OrderBy(c => c.Community.Id)
            .Skip(nextCursor)
            .Take(limit)
            .Select(c => c.Community)
            .ToList();
    }

    public async Task<List<User>> GetUserFriends(uint userId, int limit, int nextCursor)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        return user!.UserFriendUser1s.Select(uf => uf.User2)
            .OrderBy(uf => uf.Id)
            .Skip(nextCursor)
            .Take(limit)
            .ToList();
    }

    public async Task<List<Post>> GetUserPosts(uint userId, int limit, int currCursor)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        return user!.UserProfilePosts.Select(upp => upp.Post)
            .OrderBy(p => p.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
    }

    public async Task<UserProfile> GetUserProfile(uint userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        return user!.UserProfile;
    }

    public async Task<UserProfileResponseDto> ChangeUserProfile(uint userId, UserProfilePatchRequestDto userProfilePatchRequestDto)
    {
        var userProfile = await _userProfileRepository.GetAsync((userProfile) => userProfile.UserId == userId);
        bool updated = false;
        if (userProfilePatchRequestDto.ProfilePictureId != null)
        {
            var media = await _mediaRepository.GetByIdAsync((uint)userProfilePatchRequestDto.ProfilePictureId);
            if (media == null)
                throw new Exception($"Media with id equal {userProfilePatchRequestDto.ProfilePictureId} doesn't exist.");
            else
            {
                if (userProfile.ProfilePictureId != userProfilePatchRequestDto.ProfilePictureId)
                {
                userProfile.ProfilePictureId = userProfilePatchRequestDto.ProfilePictureId;
                updated = true;
                }
            }
        }
        if (userProfilePatchRequestDto.UserName != null)
        {
            if (userProfilePatchRequestDto.UserName.Length == 0)
                throw new Exception($"User name should have at east one character.");
            else
            {
                if (userProfile.UserName != userProfilePatchRequestDto.UserName)
                {
                userProfile.UserName = userProfilePatchRequestDto.UserName;
                updated = true;
                }
            }
        }
        if (userProfilePatchRequestDto.UserSurname != null)
        {
            if (userProfilePatchRequestDto.UserSurname.Length == 0)
                throw new Exception($"User surname should have at east one character.");
            else
            {
                if (userProfile.UserSurname != userProfilePatchRequestDto.UserSurname)
                {
                    userProfile.UserSurname = userProfilePatchRequestDto.UserSurname;
                    updated = true;
                }
            }
        }
        if (userProfilePatchRequestDto.UserSex != null)
        {
            if (userProfilePatchRequestDto.UserSex.Length == 0)
                throw new Exception($"User sex should have at east one character.");
            else
            {
                if (userProfile.UserSex != userProfilePatchRequestDto.UserSex)
                {
                    userProfile.UserSex = userProfilePatchRequestDto.UserSex;
                    updated = true;
                }
            }
        }
        if (userProfilePatchRequestDto.UserCountry != null)
        {
            if (userProfilePatchRequestDto.UserCountry.Length == 0)
                throw new Exception($"User country should have at east one character.");
            else
            {
                if (userProfile.UserCountry != userProfilePatchRequestDto.UserCountry)
                {
                    userProfile.UserCountry = userProfilePatchRequestDto.UserCountry;
                    updated = true;
                }
            }
        }
        if (userProfilePatchRequestDto.UserEducation != null)
        {
            if (userProfilePatchRequestDto.UserEducation.Length == 0)
                throw new Exception($"User country should have at east one character.");
            else
            {
                if (userProfile.UserEducation != userProfilePatchRequestDto.UserEducation)
                {
                    userProfile.UserEducation = userProfilePatchRequestDto.UserEducation;
                    updated = true;
                }
            }
        }
        if (updated)
        {
            userProfile!.UpdatedAt = DateTime.Now;
            _userProfileRepository.Update(userProfile!);
            await _userProfileRepository.SaveAsync();
        }
        return _mapper.Map<UserProfileResponseDto>(userProfile);
    }


}