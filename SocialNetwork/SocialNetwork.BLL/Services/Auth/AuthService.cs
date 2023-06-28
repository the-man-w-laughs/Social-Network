using AutoMapper;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Auth.Request;
using SocialNetwork.BLL.DTO.Users.Request;
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
        var users = await _userRepository.GetAllAsync(u => u.Login == login);
        return users.FirstOrDefault();
    }

    public bool IsLoginValid(string login)
    {
        return login.Length is <= Constants.UserLoginMaxLength and >= Constants.UserLoginMinLength;
    }

    public async Task<UserProfile> AddUserProfile(UserProfile userProfile)
    {
        var addedProfile = await _userProfileRepository.AddAsync(userProfile);
        await _userProfileRepository.SaveAsync();
        return addedProfile;
    }

    public async Task<UserResponseDto> SignUpUser(SignUpRequestDto userSignUpRequestDto)
    { 
        // сначала проверяем все поля на валидность 
        // далее нужно проверить еслть ли пользователь с таким логином если есть возвращаем Conflict
        // иначе генерируем соль, формируем хэш пароля записываем все в бд возвращаем ok с dto
        if (!IsLoginValid(userSignUpRequestDto.Login))
            throw new ArgumentException("Login must be between 3 and 20 characters");
        if (!_passwordHashService.IsPasswordValid(userSignUpRequestDto.Password))
            throw new ArgumentException("password must be at least 6 characters");

        var isLoginExisted = await IsLoginAlreadyExists(userSignUpRequestDto.Login);
        if (isLoginExisted) 
            throw new DuplicateEntryException("This login already exists");

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
       
       var addedUser = await AddUser(newUser);

       var userProfile = new UserProfile
       {
           CreatedAt = DateTime.Now,
           UserId = addedUser.Id
       };

       await AddUserProfile(userProfile);

       return _mapper.Map<UserResponseDto>(addedUser);
    }

    public async Task<UserResponseDto> LoginUser(LoginRequestDto userLoginRequestDto)
    {
        var user = await GetUserByLogin(userLoginRequestDto.Login);
        if (user == null) 
            throw new WrongCredentialsException("User with this login Doesn't exist");
        
        var salt = user.Salt;
        var hashedPassword = user.PasswordHash;

        var isPasswordCorrect = _passwordHashService.VerifyPassword(userLoginRequestDto.Password, salt, hashedPassword);
        if (!isPasswordCorrect) 
            throw new WrongCredentialsException("Incorrect password");
        
        return _mapper.Map<UserResponseDto>(user);
    }
}