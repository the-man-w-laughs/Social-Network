using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.DAL.Repositories.Posts;

public class PostMediaRepository : Repository<PostMedia>, IPostMediaRepository
{
    public PostMediaRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}