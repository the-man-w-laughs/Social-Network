using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.API.Controllers;

[ApiController]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
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
    public async Task<ActionResult<UserResponseDto>> SignUp(
        [FromBody, Required] UserSignUpRequestDto userSignUpRequestDto)
    {
        var addedUser = await _authService.SignUpUser(userSignUpRequestDto);
        return Ok(addedUser);
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
        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (isUserAuthenticated.Succeeded)
            throw new LoggedInUserAccessException("User is already authenticated");

        var authenticatedUser = await _authService.LoginUser(userLoginRequestDto);

        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, authenticatedUser.Id.ToString()),
            new(ClaimsIdentity.DefaultRoleClaimType, authenticatedUser.TypeId.ToString()),
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(claimsPrincipal);

        return Ok(authenticatedUser);
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
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok("Logout");
    }
}