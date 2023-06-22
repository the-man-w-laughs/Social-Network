using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Entities.Medias;

namespace SocialNetwork.DAL.Repositories.Medias;

public class MediaRepository : Repository<Media>, IMediaRepository
{
    public MediaRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}