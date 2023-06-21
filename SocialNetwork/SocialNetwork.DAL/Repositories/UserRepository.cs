using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts;
using SocialNetwork.DAL.Entities.Users;
using SocialNetwork.DAL.Repositories.Base;

namespace SocialNetwork.DAL.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) { }

    public async Task<List<User>> GetAllUsersPaginated(int limit, int? currCursor)
    {
        return await SocialNetworkContext.Users
            .OrderBy(u => u.Id)
            .Where(p => p.Id > currCursor)
            .Take(limit)
            .ToListAsync();
    }
}