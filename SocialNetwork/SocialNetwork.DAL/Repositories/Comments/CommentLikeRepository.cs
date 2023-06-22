using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Comments;
using SocialNetwork.DAL.Entities.Comments;

namespace SocialNetwork.DAL.Repositories.Comments;

public class CommentLikeRepository : Repository<CommentLike>, ICommentLikeRepository
{
    public CommentLikeRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}