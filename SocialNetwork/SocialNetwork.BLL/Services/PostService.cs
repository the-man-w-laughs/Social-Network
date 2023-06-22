using SocialNetwork.BLL.Contracts;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserProfilePostRepository _userProfilePostRepository;

    public PostService(IPostRepository postRepository, IUserProfilePostRepository userProfilePostRepository)
    {
        _postRepository = postRepository;
        _userProfilePostRepository = userProfilePostRepository;
    }

    public async Task<UserProfilePost> CreateUserProfilePost(uint userId,Post post)
    {
        var newPost = await _postRepository.AddAsync(post);
        await _postRepository.SaveAsync();
        
        var userPost = new UserProfilePost
        {
            PostId = newPost.Id,
            UserId = userId,
            
        };
        
        var newUserProfilePost = await _userProfilePostRepository.AddAsync(userPost);
        await _userProfilePostRepository.SaveAsync();
        
        return newUserProfilePost;
    }
}