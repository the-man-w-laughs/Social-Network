using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Repositories.Base;

namespace SocialNetwork.DAL.Repositories;

public class MediaRepository : Repository<Media>, IMediaRepository
{
    public MediaRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}