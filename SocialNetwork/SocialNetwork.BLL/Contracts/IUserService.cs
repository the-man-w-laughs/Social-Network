using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.Contracts;

public interface IUserService
{
    public Task<List<User>> GetUsers(int limit, int currCursor);
    Task<List<Chat>> GetUserChats(uint userId,int limit,int currCursor);
    Task<List<Community>> GetUserCommunities(uint userId, int limit, int nextCursor);
    Task<List<User>> GetUserFriends(uint userId, int limit, int nextCursor);
    Task<List<Post>> GetUserPosts(uint userId, int limit, int currCursor);
    Task<UserProfile> GetUserProfile(uint userId);
}