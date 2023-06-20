using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.Contracts;

public interface IAuthService
{
    public Task<bool> IsLoginAlreadyExists(string login);

    public Task<User> AddUser(User newUser);

    public bool IsLoginValid(string login);

    public bool IsPasswordValid(string password);

}