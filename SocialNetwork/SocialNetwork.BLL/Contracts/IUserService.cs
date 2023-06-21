using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.Contracts;

public interface IUserService
{
    public Task<List<User>> GetUsers(uint? limit, uint? currCursor);
}