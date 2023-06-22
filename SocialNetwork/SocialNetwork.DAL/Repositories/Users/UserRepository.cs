using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Repositories.Users;

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