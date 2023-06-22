using SocialNetwork.BLL.Contracts;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetUsers(int limit, int currCursor)
    {
        var users = await _userRepository.GetAllAsync();
        return users.OrderBy(u => u.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
    }

    public async Task<List<Chat>> GetUserChats(uint userId,int limit,int currCursor)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        return user!.ChatMembers.Select(cm => cm.Chat)
            .OrderBy(cm=>cm.Id)
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
}