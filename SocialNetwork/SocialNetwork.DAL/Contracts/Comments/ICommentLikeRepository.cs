using SocialNetwork.DAL.Entities.Comments;

namespace SocialNetwork.DAL.Contracts.Comments;

public interface ICommentLikeRepository : IRepository<CommentLike> {
    Task<CommentLike> LikeComment(uint userId, uint commentId);
    Task<List<CommentLike>> GetCommentLikes(uint commentId);    
}