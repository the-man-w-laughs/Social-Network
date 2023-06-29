using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Comments;
using SocialNetwork.DAL.Entities.Comments;

namespace SocialNetwork.DAL.Repositories.Comments;

public class CommentLikeRepository : Repository<CommentLike>, ICommentLikeRepository
{
    public CommentLikeRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}

    public async Task<CommentLike> LikeComment(uint userId, uint commentId)
    {
        var newLike = new CommentLike()
        {
            UserId = userId,
            CommentId = commentId,
            CreatedAt = DateTime.Now,
        };
        await AddAsync(newLike);
        await SaveAsync();
        return newLike;
    }
    public async Task<List<CommentLike>> GetCommentLikes(uint commentId)
    {
        return await GetAllAsync(commentLike => commentLike.CommentId == commentId);
    }

    public async Task<CommentLike?> UnLikeComment(uint userId, uint commentId)
    {
        var commentLike = await GetAsync(commentLike => commentLike.CommentId == commentId && commentLike.UserId == userId);
        if (commentLike != null)
        {
            Delete(commentLike);
            await SaveAsync();
        }
        return commentLike;
    }
}