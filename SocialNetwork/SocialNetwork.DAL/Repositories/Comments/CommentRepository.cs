using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Comments;
using SocialNetwork.DAL.Entities.Comments;

namespace SocialNetwork.DAL.Repositories.Comments;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}