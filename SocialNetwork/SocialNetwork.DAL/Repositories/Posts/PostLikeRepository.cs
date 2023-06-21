using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.DAL.Repositories.Posts;

public class PostLikeRepository : Repository<PostLike>, IPostLikeRepository
{
    public PostLikeRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}