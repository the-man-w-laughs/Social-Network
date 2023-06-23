using SocialNetwork.BLL.Contracts;
using SocialNetwork.DAL;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    private readonly IUserProfileRepository _userProfileRepository;

    public AuthService(IUserRepository userRepository, IUserProfileRepository userProfileRepository)
    {
        _userRepository = userRepository;
        _userProfileRepository = userProfileRepository;
    }

    public async Task<bool> IsLoginAlreadyExists(string login)
    {
        var users = await _userRepository.GetAllAsync(u => u.Login == login);
        return users.Any();
    }

    public async Task<User> AddUser(User newUser)
    {
        var user = await _userRepository.AddAsync(newUser);
        await _userRepository.SaveAsync();
        return user;
    }

    public async Task<User?> GetUserByLogin(string login)
    {
        var users = await _userRepository.GetAllAsync(u=> u.Login == login);
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

    public async  Task<UserProfile> AddUserProfile(UserProfile userProfile)
    {
        var addedProfile = await _userProfileRepository.AddAsync(userProfile);
        await _userProfileRepository.SaveAsync();
        return addedProfile;
    }
}