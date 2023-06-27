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
[Route("[controller]")]
public sealed class AdminController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly ISaltService _saltService;
    private readonly IPasswordHashService _passwordHashService;

    public AdminController(IMapper mapper, IAuthService authService, ISaltService saltService, IPasswordHashService passwordHashService)
    {
        _mapper = mapper;
        _authService = authService;
        _saltService = saltService;
        _passwordHashService = passwordHashService;
    }

    /// <summary>
    /// DeleteMedia
    /// </summary>
    /// <remarks>Delete media report.</remarks>    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("medias/{mediaId}")]
    public async Task<ActionResult<UserResponseDto>> DeleteAdminMedias([FromRoute, Required] uint mediaId)
    {
        return Ok();
    }

    /// <summary>
    /// DeleteCommunity
    /// </summary>
    /// <remarks>Delete community report.</remarks>    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("communities/{communityId}")]
    public async Task<ActionResult<UserResponseDto>> DeleteAdminCommunities([FromRoute, Required] uint communityId)
    {
        return Ok();
    }

    /// <summary>
    /// DeleteUser
    /// </summary>
    /// <remarks>Delete user report.`</remarks>    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("users/{userId}")]
    public async Task<ActionResult<UserResponseDto>> DeleteAdminUsers([FromRoute, Required] uint userId)
    {
        return Ok();
    }

    /// <summary>
    /// DeletePost
    /// </summary>
    /// <remarks>Creates a new user using the provided login, email, and password.</remarks>    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("posts/postId")]
    public async Task<ActionResult<UserResponseDto>> DeleteAdminPosts([FromRoute, Required] uint postId)
    {
        return Ok();
    }

    /// <summary>
    /// DeleteComment
    /// </summary>
    /// <remarks>Creates a new user using the provided login, email, and password.</remarks>    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("comments/commentId")]
    public async Task<ActionResult<UserResponseDto>> DeleteAdminComments([FromRoute, Required] uint commentId)
    {
        return Ok();
    }
}