using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTO;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;

    public UsersController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// GetAllUsers
    /// </summary>
    /// <remarks>Returns all users using pagination.</remarks>
    [HttpGet]        
    public virtual ActionResult<List<UserDto>> GetUsers([FromQuery][Required()]uint? limit, [FromQuery] uint? currCursor)
    {
        var users = new List<User>
        {
            new (){ Id = 1, Login = "lepesh1" },
            new (){ Id = 2, Login = "lepesh2" },
        };
        return Ok(users.Select(user=>_mapper.Map<UserDto>(user)));
    }

    /// <summary>
    /// GetAllUserChats
    /// </summary>
    /// <remarks>Get all users chats using pagination (for account owner).</remarks>    
    [HttpGet]
    [Route("{userId}/chats")]        
    public virtual IActionResult GetUsersUserIdChats([FromQuery][Required()] uint? limit, [FromQuery] uint? nextCursor)
    {
        return Ok("GetAllUserChats");
    }

    /// <summary>
    /// GetAllUserCommunities
    /// </summary>
    /// <remarks>Get user&#x27;s communities using pagination.</remarks>    
    [HttpGet]
    [Route("{userId}/communities")]
    public virtual IActionResult GetUsersUserIdCommunities([FromRoute][Required] uint userId, [FromQuery][Required()] uint? limit, [FromQuery] uint? nextCursor)
    {
        return Ok("GetAllUserCommunities");
    }

    /// <summary>
    /// GetAllUserFriends
    /// </summary>
    /// <remarks>Get all user&#x27;s friends using pagination.</remarks>    
    [HttpGet]
    [Route("{userId}/friends")]  
    public virtual IActionResult GetUsersUserIdFriends([FromRoute][Required] uint userId, [FromQuery][Required()] uint? limit, [FromQuery] uint? nextCursor)
    {
        return Ok("GetAllUserFriends");
    }

    /// <summary>
    /// GetAllUserPosts
    /// </summary>
    /// <remarks>Get all user&#x27;s posts using pagination.</remarks>    
    [HttpGet]
    [Route("{userId}/posts")]
    public virtual IActionResult GetUsersUserIdPosts([FromRoute][Required] uint userId, [FromQuery]uint? limit, [FromQuery]uint? currCursor)
    {
        return Ok("GetAllUserPosts");
    }

    /// <summary>
    /// GetUserProfileInfo
    /// </summary>
    /// <remarks>Get user&#x27;s info.</remarks>           
    [HttpGet]
    [Route("{userId}/profile")]
    public virtual IActionResult GetUsersUserIdProfile([FromRoute][Required]uint userId)
    {
        return Ok("GetUserProfileInfo");
    }

    /// <summary>
    /// ChangeUserActivityFields
    /// </summary>
    /// <remarks>Makes user&#x27;s account deactivated (for account owner or admin).</remarks>    
    [HttpPatch]
    [Route("{userId}")]
    public virtual IActionResult PatchUsersUserId([FromRoute][Required]uint userId)
    {
        return Ok("ChangeUserActivityFields");
    }

    /// <summary>
    /// ChangeUserAuthFields
    /// </summary>
    /// <remarks>Change authentification fields.</remarks>        
    [HttpPatch]
    [Route("{userId}/auth")]
    public virtual IActionResult PatchUsersUserIdAuth([FromRoute][Required]uint userId)
    {
        return Ok("ChangeUserAuthFields");
    }

    /// <summary>
    /// ChangeUserProfileInfo
    /// </summary>
    /// <remarks>Change personal info (status, sex).</remarks>        
    [HttpPatch]
    [Route("{userId}/profile")]
    public virtual IActionResult PatchUsersUserIdProfile([FromRoute][Required]uint userId)
    {
        return Ok("ChangeUserProfileInfo");
    }

    /// <summary>
    /// CreateUser
    /// </summary>
    /// <remarks>Creates new user using login and password.</remarks>    
    [HttpPost]        
    public virtual IActionResult PostUsers()
    {
        return Ok("CreateUser");
    }

    /// <summary>
    /// CreateUserPost
    /// </summary>
    /// <remarks>Create user&#x27;s post.</remarks>    
    [HttpPost]
    [Route("{userId}/posts")]
    public virtual IActionResult PostUsersUserIdPosts([FromRoute][Required]uint userId)
    {
        return Ok("CreateUserPost");
    }
}