using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.DAL.Repositories.Posts;

public class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}