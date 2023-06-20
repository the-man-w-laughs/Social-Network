using System.Text.RegularExpressions;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.DAL;
using SocialNetwork.DAL.Entities.Users;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork.BLL.Services;

public class AuthService : IAuthService
{
    private readonly UserRepository _userRepository;

    public AuthService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> IsLoginAlreadyExists(string login)
    {
        var users = await _userRepository.Select(u => u.Login == login);
        return users.Any();
    }

    public async Task<User> AddUser(User newUser)
    {
        return await _userRepository.Add(newUser);
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