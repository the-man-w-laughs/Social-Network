using AutoMapper;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUserFriendRepository _userFriendRepository;
    private readonly IUserFollowerRepository _userFollowerRepository;
    private readonly IMediaRepository _mediaRepository;

    private readonly IPasswordHashService _passwordHashService;
    private readonly IPostRepository _postRepository;

    public UserService(
        IMapper mapper,
        IUserRepository userRepository,
        IUserProfileRepository userProfileRepository,
        IUserFriendRepository userFriendRepository,
        IUserFollowerRepository userFollowerRepository,
        IMediaRepository mediaRepository,
        IPasswordHashService passwordHashService,
        IPostRepository postRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userProfileRepository = userProfileRepository;
        _userFriendRepository = userFriendRepository;
        _userFollowerRepository = userFollowerRepository;
        _mediaRepository = mediaRepository;
        _passwordHashService = passwordHashService;
        _postRepository = postRepository;
    }

    public async Task<List<UserResponseDto>> GetUsers(int limit, int currCursor)
    {
        var users = await _userRepository.GetAllAsync();
        return users.OrderBy(u => u.Id)
            .Skip(currCursor)
            .Take(limit)
            .Select(u => _mapper.Map<UserResponseDto>(u))
            .ToList();
    }

    public async Task<List<ChatResponseDto>> GetUserChats(uint userId, int limit, int currCursor)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException($"User (ID: {userId}) doesn't exist");
        
        return user.ChatMembers.Select(cm => cm.Chat)
            .OrderBy(cm => cm.Id)
            .Skip(currCursor)
            .Take(limit)
            .Select(c => _mapper.Map<ChatResponseDto>(c))
            .ToList();
    }

    public async Task<List<CommunityResponseDto>> GetUserCommunities(uint userId, int limit, int nextCursor)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException($"User (ID: {userId}) doesn't exist");
        
        return user.CommunityMembers.Select(c => c.Community)
            .OrderBy(c => c.Id)
            .Skip(nextCursor)
            .Take(limit)
            .Select(c => _mapper.Map<CommunityResponseDto>(c))
            .ToList();
    }
    public async Task<List<CommunityResponseDto>> GetUserManagedCommunities(uint userId, int limit, int nextCursor)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException($"User (ID: {userId}) doesn't exist");
        
        return user.CommunityMembers
            .Where(c => c.TypeId is CommunityMemberType.Admin or CommunityMemberType.Owner)
            .OrderBy(c => c.Community.Id)
            .Skip(nextCursor)
            .Take(limit)
            .Select(c => c.Community)
            .Select(c => _mapper.Map<CommunityResponseDto>(c))
            .ToList();
    }

    public async Task<List<UserResponseDto>> GetUserFriends(uint userId, int limit, int nextCursor)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException($"User (ID: {userId}) doesn't exist");
        
        return user.UserFriendUser1s.Select(uf => uf.User2)
            .OrderBy(uf => uf.Id)
            .Skip(nextCursor)
            .Take(limit)
            .Select(u => _mapper.Map<UserResponseDto>(u))
            .ToList();
    }

    public async Task<List<PostResponseDto>> GetUserPosts(uint userId, int limit, int currCursor)
    {
        var posts = await _postRepository.GetAllAsync(post => post.AuthorId == userId && !post.IsCommunityPost);
        var paginatedPosts = posts.OrderBy(cm => cm.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
        return _mapper.Map<List<PostResponseDto>>(paginatedPosts);
    }

    public async Task<UserProfileResponseDto> GetUserProfile(uint userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException($"User (ID: {userId}) doesn't exist");
        
        return _mapper.Map<UserProfileResponseDto>(user.UserProfile);
    }

    public async Task<UserProfileResponseDto> ChangeUserProfile(uint userId, UserProfilePatchRequestDto userProfilePatchRequestDto)
    {
        var userProfile = await _userProfileRepository.GetAsync(userProfile => userProfile.UserId == userId);
        if (userProfile == null)
            throw new NotFoundException($"User (ID: {userId}) doesn't exist");
        
        var updated = false;
        if (userProfilePatchRequestDto.ProfilePictureId != null)
        {
            var media = await _mediaRepository.GetByIdAsync(userProfilePatchRequestDto.ProfilePictureId.Value);
            if (media == null)
                throw new ArgumentException($"Media (ID: {userProfilePatchRequestDto.ProfilePictureId}) doesn't exist.");
            
            if (userProfile.ProfilePictureId != userProfilePatchRequestDto.ProfilePictureId)
            {
                userProfile.ProfilePictureId = userProfilePatchRequestDto.ProfilePictureId;
                updated = true;
            }
        }

        if (userProfilePatchRequestDto.UserName != null)
        {
            if (string.IsNullOrWhiteSpace(userProfilePatchRequestDto.UserName))
                throw new ArgumentException("User name should have at least 1 character without whitespaces.");
            
            if (userProfile.UserName != userProfilePatchRequestDto.UserName)
            {
                userProfile.UserName = userProfilePatchRequestDto.UserName;
                updated = true;
            }
        }

        if (userProfilePatchRequestDto.UserSurname != null)
        {
            if (string.IsNullOrWhiteSpace(userProfilePatchRequestDto.UserSurname))
                throw new ArgumentException("User surname should have at least 1 character without whitespaces.");
            
            if (userProfile.UserSurname != userProfilePatchRequestDto.UserSurname)
            {
                userProfile.UserSurname = userProfilePatchRequestDto.UserSurname;
                updated = true;
            }
        }

        if (userProfilePatchRequestDto.UserSex != null)
        {
            if (string.IsNullOrWhiteSpace(userProfilePatchRequestDto.UserSex))
                throw new ArgumentException("User sex should have at least 1 character without whitespaces.");
            
            if (userProfile.UserSex != userProfilePatchRequestDto.UserSex)
            {
                userProfile.UserSex = userProfilePatchRequestDto.UserSex;
                updated = true;
            }
        }

        if (userProfilePatchRequestDto.UserCountry != null)
        {
            if (string.IsNullOrWhiteSpace(userProfilePatchRequestDto.UserCountry))
                throw new ArgumentException("User country should have at least 1 character without whitespaces.");
            
            if (userProfile.UserCountry != userProfilePatchRequestDto.UserCountry)
            {
                userProfile.UserCountry = userProfilePatchRequestDto.UserCountry;
                updated = true;
            }
        }

        if (userProfilePatchRequestDto.UserEducation != null)
        {
            if (string.IsNullOrWhiteSpace(userProfilePatchRequestDto.UserEducation))
                throw new ArgumentException("User education should have at least 1 character without whitespaces.");
            
            if (userProfile.UserEducation != userProfilePatchRequestDto.UserEducation)
            {
                userProfile.UserEducation = userProfilePatchRequestDto.UserEducation;
                updated = true;
            }
        }

        if (updated)
        {
            userProfile.UpdatedAt = DateTime.Now;
            _userProfileRepository.Update(userProfile);
            await _userProfileRepository.SaveAsync();
        }

        return _mapper.Map<UserProfileResponseDto>(userProfile);
    }

    public async Task<User?> GetUserById(uint userId)
    {
        return await _userRepository.GetByIdAsync(userId);
    }

    public async Task<bool> IsUserYourFriend(uint userId, uint userFriendId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        var userFriends1 = user!.UserFriendUser1s.FirstOrDefault(uf => uf.User2Id == userFriendId);
        var userFriends2 = user!.UserFriendUser2s.FirstOrDefault(uf => uf.User1Id == userFriendId);
        return userFriends1 != null || userFriends2 != null;
    }

    public async Task<bool> IsUserYourFollower(uint userId, uint userFollowerId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        var userFollowers = user!.UserFollowerTargets.FirstOrDefault(uf => uf.SourceId == userFollowerId);
        return userFollowers != null;
    }

    public async Task<UserFollower> DeleteFollowerFromDatabase(uint userId, uint id)
    {
        var follower = await _userFollowerRepository.GetAsync
            (uf=>uf.TargetId == userId && uf.SourceId == id);
        _userFollowerRepository.Delete(follower!);
        await _userFollowerRepository.SaveAsync();
        return follower!;
    }

    public async Task<UserProfileResponseDto> AddFriend(uint userId, uint friendId)
    {
        var user = await GetUserById(userId);
        if (user == null)
            throw new NotFoundException($"User (ID {userId}) doesn't exist");

        var friend = await GetUserById(friendId);
        if (friend == null)
            throw new NotFoundException($"Friend (ID: {friendId}) doesn't exist");

        if (userId == friendId)
            throw new ArgumentException("User can't add yourself to friends");

        var isUserYourFriend = await IsUserYourFriend(userId, friendId);
        if (isUserYourFriend)
            throw new DuplicateEntryException("User is already your friend");

        var isUserYourFollower = await IsUserYourFollower(userId, friendId);
        if (isUserYourFollower)
        {
            await DeleteFollowerFromDatabase(userId, friendId);
            var addedFriend = await AddFriendToDatabase(userId, friendId);
            return _mapper.Map<UserProfileResponseDto>(addedFriend.User2.UserProfile);
        }

        var addedFollower = await Follow(userId, friendId);
        
        return _mapper.Map<UserProfileResponseDto>(addedFollower.Source.UserProfile);
    }

    private async Task<UserFriend> AddFriendToDatabase(uint userId, uint friendId)
    {
        var newFriend = new UserFriend
        {
            CreatedAt = DateTime.Now,
            User1Id = userId,
            User2Id = friendId
        };
        var addedFriend = await _userFriendRepository.AddAsync(newFriend);
        await _userFriendRepository.SaveAsync();
        return addedFriend;
    }

    private async Task<UserFollower> Follow(uint sourceId, uint targetId)
    {
        var newFollower = new UserFollower
        {
            CreatedAt = DateTime.Now,
            SourceId = sourceId,
            TargetId = targetId
        };
        var addedFollower = await _userFollowerRepository.AddAsync(newFollower);
        await _userFollowerRepository.SaveAsync();
        return addedFollower;
    }

    public async Task DeleteFriendship(uint userId, uint id)
    {
        var friendship = await _userFriendRepository.GetAsync
        (friend => (friend.User1Id == userId && friend.User2Id == id) ||
                   (friend.User1Id == id && friend.User2Id == userId));
        _userFriendRepository.Delete(friendship!);
        await _userFriendRepository.SaveAsync();
    }

    public async Task<List<UserResponseDto>> GetUserFollowers(uint userId, int limit, int currCursor)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        return user!.UserFollowerTargets.Select(uf => uf.Source)
            .OrderBy(uf => uf.Id)
            .Skip(currCursor)
            .Take(limit)
            .Select(u => _mapper.Map<UserResponseDto>(u))
            .ToList();
    }

    public async Task<UserProfileResponseDto> DeleteFriend(uint userId, uint friendId)
    {
        var user = await GetUserById(userId);
        if (user == null)
            throw new NotFoundException($"User (ID {userId}) doesn't exist");
        
        var friend = await GetUserById(friendId);
        if (friend == null)
            throw new NotFoundException($"Friend (ID: {friendId}) doesn't exist");

        var isUserYourFriend = await IsUserYourFriend(userId, friendId);
        if (!isUserYourFriend)
            throw new DuplicateEntryException("User is not your friend");

        await DeleteFriendship(userId, friendId);
        var follower = await Follow(friendId, userId);

        return _mapper.Map<UserProfileResponseDto>(follower.Source.UserProfile);
    }

    public async Task<UserProfileResponseDto> DeleteFollower(uint userId, uint followerId)
    {
        var user = await GetUserById(userId);
        if (user == null)
            throw new NotFoundException($"User (ID {userId}) doesn't exist");
        
        var friend = await GetUserById(followerId);
        if (friend == null)
            throw new NotFoundException($"Follower (ID: {followerId}) doesn't exist");

        var isUserYourFollower = await IsUserYourFollower(userId, followerId);
        if (!isUserYourFollower)
            throw new DuplicateEntryException("User is not your Follower");

        var deletedFollower = await DeleteFollowerFromDatabase(userId, followerId);

        return _mapper.Map<UserProfileResponseDto>(deletedFollower.Source.UserProfile);
    }

    public async Task<UserResponseDto> ChangeUserLogin(uint userId, UserLoginRequestDto userLoginRequestDto)
    {
        var user = await GetUserById(userId);
        if (user == null)
            throw new NotFoundException($"User (ID {userId}) doesn't exist");

        if (user.Login == userLoginRequestDto.Login)
            throw new ArgumentException($"$User (ID {userId}) already has login \"{user.Login}\"");

        if (string.IsNullOrWhiteSpace(userLoginRequestDto.Login))
            throw new ArgumentException("User login should have at least 1 character without whitespaces.");
        
        user.Login = userLoginRequestDto.Login;
        _userRepository.Update(user);
        await _userRepository.SaveAsync();

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto> ChangeUserPassword(uint userId, UserPasswordRequestDto userPasswordRequestDto)
    {
        var user = await GetUserById(userId);
        if (user == null)
            throw new NotFoundException($"User (ID {userId}) doesn't exist");
        
        if (_passwordHashService.IsPasswordValid(userPasswordRequestDto.Password))
            throw new ArgumentException($"User password should have at least {Constants.UserPasswordMinLength} character without whitespaces.");

        var isPreviousPasswordCorrect = _passwordHashService.VerifyPassword(
            userPasswordRequestDto.PreviousPassword, user.Salt, user.PasswordHash);

        if (!isPreviousPasswordCorrect)
            throw new AccessDeniedException("Previous password is incorrect");

        user.Salt = _passwordHashService.GenerateSalt();
        user.PasswordHash = _passwordHashService.HashPassword(userPasswordRequestDto.Password, user.Salt);
        
        _userRepository.Update(user);
        await _userRepository.SaveAsync();

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto> ChangeUserEmail(uint userId, UserEmailRequestDto userEmailRequestDto)
    {
        var user = await GetUserById(userId);
        if (user == null)
            throw new NotFoundException($"User (ID {userId}) doesn't exist");
        
        if (string.IsNullOrWhiteSpace(userEmailRequestDto.Email))
            throw new ArgumentException("User email should have at least 1 character without whitespaces.");

        user.Email = userEmailRequestDto.Email;
        
        _userRepository.Update(user);
        await _userRepository.SaveAsync();

        return _mapper.Map<UserResponseDto>(user);
    }
}