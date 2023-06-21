using SocialNetwork.BLL.Contracts;
using SocialNetwork.DAL;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> IsLoginAlreadyExists(string login)
    {
        var users = await _userRepository.GetAllAsync(u => u.Login == login);
        return users.Any();
    }

    public async Task AddUser(User newUser)
    {
        await _userRepository.AddAsync(newUser);
        await _userRepository.SaveAsync();        
    }

    public async Task<User?> GetUserByLogin(string login)
    {
        var users = await  _userRepository.GetAllAsync(u=> u.Login == login);
        return users.FirstOrDefault();
    }

    public bool IsLoginValid(string login)
    {
        return login.Length is <= Constants.UserLoginMaxLength and >= Constants.UserLoginMinLength;
    }

    public bool IsPasswordValid(string password)
    {
        return password.Length >= Constants.UserPasswordMinLength;
    }
    
}