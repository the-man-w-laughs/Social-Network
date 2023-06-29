using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;

namespace SocialNetwork.BLL.Contracts;

public interface IPostService
{
    Task<PostResponseDto> CreatePost(uint userId, PostRequestDto postRequestDto);
    Task<PostResponseDto> GetPost(uint userId, object postRequestDto);    
    Task<PostResponseDto> ChangePost(uint userId, PostPatchRequestDto postPatchRequestDto);
    Task<PostResponseDto> DeletePost(uint userId, uint postId);
    Task<List<CommentResponseDto>> GetComments(uint postId, int limit, int currCursor);
    Task<PostLikeResponseDto> LikePost(uint userId, uint postId);
    Task<List<PostLikeResponseDto>> GetLikes(uint postId, int limit, int currCursor);
    Task<PostLikeResponseDto> UnlikePost(uint userId, uint postId);
}