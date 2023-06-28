using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;

namespace SocialNetwork.BLL.Contracts;

public interface IPostService
{
    public Task<UserProfilePostResponseDto> CreateUserProfilePost(uint userId, PostRequestDto postRequestDto);
}