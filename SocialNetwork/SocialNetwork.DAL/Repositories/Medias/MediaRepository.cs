using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.DAL.Repositories.Medias;

public class MediaRepository : Repository<Media>, IMediaRepository
{
    public MediaRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}

    public async Task<Media> AddMedia(uint userId, string filePath, string fileName)
    {
        var newMedia = new Media()
        {
            FileName = fileName,
            FilePath = filePath,
            MediaTypeId = MediaType.Image,
            CreatedAt = DateTime.Now,
            OwnerId = userId
        };
        await AddAsync(newMedia);

        await SaveAsync();

        return newMedia;
    }
}