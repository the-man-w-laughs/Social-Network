using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;

namespace SocialNetwork.BLL.Contracts;

public interface IUserService
{
    Task<List<UserResponseDto>> GetUsers(int limit, int currCursor);
    Task<List<ChatResponseDto>> GetUserChats(uint userId,int limit, int currCursor);
    Task<List<CommunityResponseDto>> GetUserCommunities(uint userId, int limit, int nextCursor);
    Task<List<CommunityResponseDto>> GetUserManagedCommunities(uint userId, int limit, int nextCursor);
    Task<List<UserResponseDto>> GetUserFriends(uint userId, int limit, int nextCursor);
    Task<List<PostResponseDto>> GetUserPosts(uint userId, int limit, int currCursor);
    Task<UserProfileResponseDto> GetUserProfile(uint userId);
    Task<UserProfileResponseDto> ChangeUserProfile(uint userId, UserProfilePatchDto userProfileRequestDto);
    Task<UserProfileResponseDto> AddFriend(uint userId, uint friendId);
    Task<List<UserResponseDto>> GetUserFollowers(uint userId, int limit, int currCursor);
    Task<UserProfileResponseDto> DeleteFriend(uint userId, uint friendId);
    Task<UserProfileResponseDto> DeleteFollower(uint userId, uint followerId);
    Task<UserResponseDto> ChangeUserLogin(uint userId, UserLoginPutDto userLoginRequestDto);
    Task<UserResponseDto> ChangeUserPassword(uint userId, UserPasswordPutDto userPasswordRequestDto);
    Task<UserResponseDto> ChangeUserEmail(uint userId, UserEmailPutDto userEmailRequestDto);
    Task<UserResponseDto> GetUserAccount(uint userId);
}