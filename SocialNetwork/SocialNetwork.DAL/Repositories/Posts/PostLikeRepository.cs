using DocumentFormat.OpenXml.Office2010.PowerPoint;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.DAL.Repositories.Posts;

public class PostLikeRepository : Repository<PostLike>, IPostLikeRepository
{
    public PostLikeRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {
    }

    public async Task<PostLike> LikeComment(uint userId, uint postId)
    {
        var newLike = new PostLike()
        {
            UserId = userId,
            PostId = postId,
            CreatedAt = DateTime.Now,
        };
        await AddAsync(newLike);
        await SaveAsync();
        return newLike;
    }
}