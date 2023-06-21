using SocialNetwork.DAL.Contracts.Base;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Contracts;

public interface IUserRepository : IRepository<User>
{
    public Task<List<User>> GetAllUsersPaginated(int limit, int? currCursor);
}