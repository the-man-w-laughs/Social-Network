using AutoMapper;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Auth.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.BLL.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IPasswordHashService _passwordHashService;

    public AuthService(IMapper mapper,
        IUserRepository userRepository,
        IUserProfileRepository userProfileRepository,
        IPasswordHashService passwordHashService)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userProfileRepository = userProfileRepository;
        _passwordHashService = passwordHashService;
    }
    
    public async Task<UserResponseDto> SignUp(SignUpPostDto userSignUpRequestDto)
    {        
        var isLoginExisted = await IsLoginAlreadyExists(userSignUpRequestDto.Login);
        if (isLoginExisted) 
            throw new DuplicateEntryException($"Login \"{userSignUpRequestDto.Login}\" already exists");

        var salt = _passwordHashService.GenerateSalt();
        var hashedPassword = _passwordHashService.HashPassword(userSignUpRequestDto.Password, salt);
       
        var newUser = new User
        {
            Login = userSignUpRequestDto.Login,
            PasswordHash = hashedPassword,
            Email = userSignUpRequestDto.Email,
            Salt = salt,
            TypeId = UserType.User,
            CreatedAt = DateTime.Now
        };       
       
        var addedUser = await _userRepository.AddAsync(newUser);
        await _userRepository.SaveAsync();
        
       var userProfile = new UserProfile
       {
           CreatedAt = DateTime.Now,
           UserId = addedUser.Id
       };

       await _userProfileRepository.AddAsync(userProfile);
       await _userProfileRepository.SaveAsync();

       return _mapper.Map<UserResponseDto>(addedUser);
    }

    public async Task<UserResponseDto> Login(LoginPostDto userLoginRequestDto)
    {
        var user = await _userRepository.GetAsync(u => u.Login == userLoginRequestDto.Login);
        if (user == null) 
            throw new WrongCredentialsException($"User with login \"{userLoginRequestDto.Login}\" doesn't exist");
        
        var salt = user.Salt;
        var hashedPassword = user.PasswordHash;

        var isPasswordCorrect = _passwordHashService.VerifyPassword(userLoginRequestDto.Password, salt, hashedPassword);
        if (!isPasswordCorrect) 
            throw new WrongCredentialsException("Incorrect password");
        
        return _mapper.Map<UserResponseDto>(user);
    }

    private async Task<bool> IsLoginAlreadyExists(string login)
    {
        var user = await _userRepository.GetAsync(u => u.Login == login);
        return user != null;
    }
}