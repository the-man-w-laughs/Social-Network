using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.Contracts;

public interface IPostService
{
    public Task<UserProfilePost> CreateUserProfilePost(uint UserId, Post post);
}