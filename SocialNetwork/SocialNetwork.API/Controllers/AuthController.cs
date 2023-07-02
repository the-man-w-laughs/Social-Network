using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.DTO.Auth.Request;
using SocialNetwork.BLL.DTO.Users.Response;

namespace SocialNetwork.API.Controllers;

[ApiController]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>Sign Up</summary>
    /// <remarks>Creates a new user and his profile using the provided email, login, and password.</remarks>
    /// <param name="userSignUpRequestDto">The user sign-up request data transfer object.</param>    
    /// <response code="200">Returns a <see cref="UserResponseDto"/> with details of successfully created user.</response>
    /// <response code="400">Returns a string message if the login or password is invalid.</response>
    /// <response code="409">Returns a string message if the user with the same login already exists.</response>
    [HttpPost, Route("sign-up")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserResponseDto>> SignUp([FromBody, Required] SignUpPostDto userSignUpRequestDto)
    {
        var addedUserDto = await _authService.SignUp(userSignUpRequestDto);
        return Ok(addedUserDto);
    }

    /// <summary>Login</summary>
    /// <remarks>Login user using login and password.</remarks>
    /// <param name="userLoginRequestDto">The user login request data transfer object.</param>    
    /// <response code="200">Returns a <see cref="UserResponseDto"/> with details the user logged in.</response>
    /// <response code="400">Returns a string message if the login or password is incorrect.</response>
    /// <response code="409">Returns a string message if the user is already authenticated.</response>    
    [HttpPost, Route("login")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserResponseDto>> Login([FromBody, Required] LoginPostDto userLoginRequestDto)
    {
        var authenticatedUserDto = await AuthenticateUser(userLoginRequestDto);
        return Ok(authenticatedUserDto);
    }

    /// <summary>Logout</summary>
    /// <remarks>Logs out the current user.</remarks>
    /// <response code="200">Returns a string message indicated successful logout.</response>
    /// <response code="401">Returns a string message if the user unauthorized.</response>
    [Authorize(Roles = "User")]
    [HttpPost, Route("logout")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<string>> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok("Logout was successful");
    }
    
    private async Task<UserResponseDto> AuthenticateUser(LoginPostDto userLoginRequestDto)
    {
        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (isUserAuthenticated.Succeeded)
            throw new LoggedInUserAccessException("User is already authenticated");

        var authenticatedUser = await _authService.Login(userLoginRequestDto);

        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, authenticatedUser.Id.ToString()),
            new(ClaimsIdentity.DefaultRoleClaimType, authenticatedUser.TypeId.ToString()),
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(claimsPrincipal);
        return authenticatedUser;
    }
}