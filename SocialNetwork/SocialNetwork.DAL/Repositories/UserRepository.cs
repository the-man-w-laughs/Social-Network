using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts;
using SocialNetwork.DAL.Entities.Users;
using SocialNetwork.DAL.Repositories.Base;

namespace SocialNetwork.DAL.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}

    public List<User> GetAllUsersPaginated(int limit, int? currCursor)
    {
        return SocialNetworkContext.Users
            .OrderBy(u => u.Id)
            .Where(p => p.Id > currCursor)
            .Take(limit)
            .ToList();
    }
}