using AutoMapper;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.Services;

public class PostService : IPostService
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly IUserProfilePostRepository _userProfilePostRepository;

    public PostService(IMapper mapper, IPostRepository postRepository, IUserProfilePostRepository userProfilePostRepository)
    {
        _mapper = mapper;
        _postRepository = postRepository;
        _userProfilePostRepository = userProfilePostRepository;
    }

    public async Task<UserProfilePostResponseDto> CreateUserProfilePost(uint userId, PostRequestDto postRequestDto)
    {
        var newUserPost = new Post
        {
            Content = postRequestDto.Content,
            CreatedAt = DateTime.Now
        };
        
        var newPost = await _postRepository.AddAsync(newUserPost);
        await _postRepository.SaveAsync();
        
        var userPost = new UserProfilePost
        {
            PostId = newPost.Id,
            UserId = userId,
        };
        
        var newUserProfilePost = await _userProfilePostRepository.AddAsync(userPost);
        await _userProfilePostRepository.SaveAsync();
        
        return _mapper.Map<UserProfilePostResponseDto>(newUserProfilePost);
    }
}