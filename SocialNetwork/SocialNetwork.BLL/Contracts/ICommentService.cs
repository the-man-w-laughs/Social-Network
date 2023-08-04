using SocialNetwork.BLL.DTO.Comments.Request;
using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.BLL.Exceptions;

namespace SocialNetwork.BLL.Contracts
{
    public interface ICommentService
    {
        Task<CommentResponseDto> GetComment(uint commentId);
        Task<CommentResponseDto> AddComment(uint userId, CommentPostDto commentRequestDto);
        Task<CommentResponseDto> ChangeComment(uint userId, uint commentId, CommentPatchDto commentRequestDto);
        Task<CommentResponseDto> DeleteComment(uint userId, uint commentId);
        Task<CommentLikeResponseDto> LikeComment(uint userId, uint commentId);
        Task<List<CommentLikeResponseDto>> GetCommentLikes(uint commentId, int limit, int currCursor);
        Task<CommentLikeResponseDto> UnlikeComment(uint userId, uint commentId);
    }
}
