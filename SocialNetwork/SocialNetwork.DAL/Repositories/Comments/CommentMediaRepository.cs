using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Comments;
using SocialNetwork.DAL.Entities.Comments;

namespace SocialNetwork.DAL.Repositories.Comments;

public class CommentMediaRepository : Repository<CommentMedia>, ICommentMediaRepository
{
    public CommentMediaRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}