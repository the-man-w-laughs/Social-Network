using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts;
using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Repositories.Base;

namespace SocialNetwork.DAL.Repositories;

public class CommunityRepository : Repository<Community>, ICommunityRepository
{
    public CommunityRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}