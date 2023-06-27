using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.API.Controllers;

[ApiController]
public sealed class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly ISaltService _saltService;
    private readonly IPasswordHashService _passwordHashService;

    public AuthController(IMapper mapper, IAuthService authService, ISaltService saltService, IPasswordHashService passwordHashService)
    {
        _mapper = mapper;
        _authService = authService;
        _saltService = saltService;
        _passwordHashService = passwordHashService;
    }

    /// <summary>
    /// Sign up.
    /// </summary>
    /// <remarks>Creates a new user using the provided login, email, and password.</remarks>
    /// <param name="userSignUpRequestDto">The user sign-up request data transfer object.</param>    
    /// <response code="200">Returns a <see cref="UserResponseDto"/> if the user was successfully created.</response>
    /// <response code="400">Returns a string message if the login or password is invalid.</response>
    /// <response code="409">Returns a string message if a user with the same login already exists.</response>
    [HttpPost]
    [Route("sign-up")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserResponseDto>> SignUp([FromBody, Required] UserSignUpRequestDto userSignUpRequestDto)
    {
        // сначала проверяем все поля на валидность 
        // далее нужно проверить еслть ли пользователь с таким логином если есть возвращаем Conflict
        // иначе генерируем соль, формируем хэш пароля записываем все в бд возвращаем ok с dto

        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (isUserAuthenticated.Succeeded) return Conflict("User is already authenticated");

        if (!_authService.IsLoginValid(userSignUpRequestDto.Login)) return BadRequest("Invalid Login");
        if (!_authService.IsPasswordValid(userSignUpRequestDto.Password)) return BadRequest("Invalid Password");

        var isLoginExisted = await _authService.IsLoginAlreadyExists(userSignUpRequestDto.Login);
        if (isLoginExisted) return Conflict("User with this login Already Exist");

        var salt = _saltService.GenerateSalt();
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
        
        var addedUser = await _authService.AddUser(newUser);

        var userProfile = new UserProfile()
        {
            CreatedAt = DateTime.Now,
            UserId = addedUser.Id
        };

        await _authService.AddUserProfile(userProfile);
        
        return Ok(_mapper.Map<UserResponseDto>(newUser));
        
    }

    /// <summary>
    /// Login.
    /// </summary>
    /// <remarks>Login user using login and password.</remarks>
    /// <param name="userLoginRequestDto">The user login request data transfer object.</param>    
    /// <response code="200">Returns a <see cref="UserResponseDto"/> if the login was successful.</response>
    /// <response code="400">Returns a string message if the login or password is invalid.</response>
    /// <response code="401">Returns a string message if the password is incorrect.</response>
    /// <response code="409">Returns a string message if a user is already authenticated.</response>    
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserResponseDto>> Login([FromBody, Required] UserLoginRequestDto userLoginRequestDto)
    {
        // проверяем не авторизован ли пользователь уже
        // провреяем есть ли такой логин если нет возвращаем ошибку авторизации
        // если есть берем введенный пароль прибавляем соль из бд хэшим и сверяем с хэшированным паролем в бд
        // если пароль неверный возвращаем ошибку авторизации
        // иначе ok с dto
        // выставляем куки либо header auth
        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        if (isUserAuthenticated.Succeeded) return Conflict("User is already authenticated");

        var user = await _authService.GetUserByLogin(userLoginRequestDto.Login);

        var isUserExisted = user != null;
        if (!isUserExisted) return BadRequest("User with this login Doesn't exist");

        var salt = user!.Salt;
        var hashedPassword = user.PasswordHash;

        var isPasswordCorrect = _passwordHashService.VerifyPassword(userLoginRequestDto.Password, salt, hashedPassword);
        if (!isPasswordCorrect) return Unauthorized("Incorrect password");

        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
            new(ClaimsIdentity.DefaultRoleClaimType, UserType.User.ToString()),
        };

        if (user.TypeId == UserType.Admin)
        {
            claims.Add(new(ClaimsIdentity.DefaultRoleClaimType, UserType.Admin.ToString()));
        }        
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        
        await HttpContext.SignInAsync(claimsPrincipal);

        return Ok(_mapper.Map<UserResponseDto>(user));
    }

    /// <summary>
    /// Logout.
    /// </summary>
    /// <remarks>Logs out the current user.</remarks>    
    /// <response code="200">Returns a string message indicating successful logout.</response>S
    [HttpPost]
    [Authorize(Roles = "User")]
    [Route("logout")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> Logout()
    {
        // доступ только для авторизованных пользователей
        // просто снимаем куки или headers
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok("Logout");
    }
}