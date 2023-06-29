using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;

namespace SocialNetwork.BLL.Contracts;

public interface IPostService
{
    Task<PostResponseDto> CreatePost(uint userId, PostRequestDto postRequestDto);
    Task<List<CommentResponseDto>> GetComments(uint postId, int limit, int currCursor);
    Task<List<PostLikeResponseDto>> GetLikes(uint postId, int limit, int currCursor);
    Task<PostLikeResponseDto> LikePost(uint userId, uint postId);
    Task<PostLikeResponseDto> UnlikePost(uint userId, uint postId);
}