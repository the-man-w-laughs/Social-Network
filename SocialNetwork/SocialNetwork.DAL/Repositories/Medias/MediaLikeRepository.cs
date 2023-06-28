using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.DAL.Repositories.Medias;
public class MediaLikeRepository : Repository<MediaLike>, IMediaLikeRepository
{
    public MediaLikeRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) { }

    public async Task<MediaLike> LikeMedia(uint userId, uint mediaId)
    {
        var newLike = new MediaLike()
        {
            UserId = userId,
            MediaId = mediaId,
            CreatedAt = DateTime.Now,
        };
        await AddAsync(newLike);
        await SaveAsync();
        return newLike;
    }
    public async Task<List<MediaLike>> GetMediaLikes(uint mediaId)
    {
        return await GetAllAsync(mediaLike => mediaLike.MediaId == mediaId);
    }

    public async Task<MediaLike?> UnLikeMedia(uint userId, uint mediaId)
    {
        var mediaLike = await GetAsync(mediaLike => mediaLike.MediaId == mediaId && mediaLike.UserId == userId);
        if (mediaLike != null)
        {
            Delete(mediaLike);
            await SaveAsync();
        }
        return mediaLike;
    }
}