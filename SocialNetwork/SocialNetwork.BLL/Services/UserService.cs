using SocialNetwork.BLL.Contracts;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetUsers(uint? limit, uint? currCursor)
    {
        throw new NotImplementedException();
    }
}