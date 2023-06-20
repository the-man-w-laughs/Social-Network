using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.API.Controllers;

[ApiController]
public sealed class AuthController : ControllerBase
{
    
    private readonly IAuthService _authService;

    private readonly IPasswordHashService _passwordHashService;

    private readonly ISaltService _saltService;

    private readonly IMapper _mapper;

    private const string Paper = "LG2jj21ma1OPnqizQbjBpq14B1l1zM";

    public AuthController(IAuthService authService, IPasswordHashService passwordHashService, ISaltService saltService, IMapper mapper)
    {
        _authService = authService;
        _passwordHashService = passwordHashService;
        _saltService = saltService;
        _mapper = mapper;
    }

    /// <summary>
    /// SignUp
    /// </summary>
    /// <remarks>Creates new user using login, Mail and Password.</remarks>    
    [HttpPost]
    [Route("sign-up")]
    public async Task<ActionResult<UserResponseDto>> SignUp([FromBody] [Required] UserSignUpRequestDto userSignUpRequestDto)
    {
        // сначала проверяем все поля на валидность 
        // далее нужно проверить еслть ли пользователь с таким логином если есть возвращаем Conflict
        // иначе генерируем соль, формируем хэш пароля записываем все в бд возвращаем ok с dto
        // выставляем куки либо header auth

        if (!_authService.IsLoginValid(userSignUpRequestDto.Login))
        {
            return BadRequest("Invalid Login");
        }

        if (!_authService.IsPasswordValid(userSignUpRequestDto.Password))
        {
            return BadRequest("Invalid Password");
        }
        
        var isLoginExisted = await _authService.IsLoginAlreadyExists(userSignUpRequestDto.Login);
        if (isLoginExisted)
        {
            return Conflict("User with this login Already Exist");
        }

        var salt = _saltService.GenerateSalt();

        var hashedPassword = _passwordHashService.;

        var newUser = new User
        {
            Login = userSignUpRequestDto.Login,
            PasswordHash = hashedPassword,
            Email = userSignUpRequestDto.Email
        };

        var responseUser = await _authService.AddUser(newUser);

        return Ok(_mapper.Map<UserResponseDto>(responseUser));
        
    }

    /// <summary>
    /// Login
    /// </summary>
    /// <remarks>Login user using login and password.</remarks>    
    [HttpPost]
    [Route("login")]
    public ActionResult<UserResponseDto> Login([FromBody] [Required] UserSignUpRequestDto userSignUpRequestDto)
    {
        // провреяем есть ли такой логин если нет возвращаем ошибку авторизации
        // если есть берем введенный пароль прибавляем соль из бд хэшим и сверяем с хэшированным паролем в бд
        // если пароль неверный возвращаем ошибку авторизации
        // иначе ok с dto
        // выставляем куки либо header auth
        return Ok();
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <remarks>logout.</remarks>    
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    [Route("logout")]
    public ActionResult<UserResponseDto> Logout([FromBody][Required] UserSignUpRequestDto userSignUpRequestDto)
    {
        // доступ только для авторизованных пользователей
        // просто снимаем куки или headers
        return Ok();
    }
    
    
    
    
}