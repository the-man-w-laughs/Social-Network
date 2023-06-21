using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts;
using SocialNetwork.DAL.Entities.Users;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;
    
    public UsersController(IMapper mapper, IUserRepository userRepository, IPasswordHashService passwordHashService)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
    }

    /// <summary>
    /// GetAllUsers
    /// </summary>
    /// <remarks>Returns all users using pagination.</remarks>
    [HttpGet]        
    public virtual ActionResult<List<UserProfileResponseDto>> GetUsers([FromQuery][Required()]uint? limit, [FromQuery] uint? currCursor)
    {
        var users = new List<UserProfile>
        {
            new (){ UserSex = "helicopter", UserName = "Zhanna" },
            new (){UserSex = "Mig - 29", UserName = "Palina"},
        };
        return Ok(users.Select(user => _mapper.Map<UserProfileResponseDto>(user)));        
    }

    /// <summary>
    /// GetAllUserChats
    /// </summary>
    /// <remarks>Get all users chats using pagination (for account owner).</remarks>    
    [HttpGet]
    [Route("{userId}/chats")]        
    public virtual ActionResult<List<ChatResponseDto>> GetUsersUserIdChats([FromQuery][Required()] uint? limit, [FromQuery] uint? nextCursor)
    {
        return Ok(new List<ChatResponseDto>() { new ChatResponseDto()});
    }

    /// <summary>
    /// GetAllUserCommunities
    /// </summary>
    /// <remarks>Get user's communities using pagination.</remarks>    
    [HttpGet]
    [Route("{userId}/communities")]
    public virtual ActionResult<List<CommunityResponseDto>> GetUsersUserIdCommunities([FromRoute][Required] uint userId, [FromQuery][Required()] uint? limit, [FromQuery] uint? nextCursor)
    {
        return Ok(new List<CommunityResponseDto>(){ new CommunityResponseDto()});
    }

    /// <summary>
    /// GetAllUserFriends
    /// </summary>
    /// <remarks>Get all user's friends using pagination.</remarks>    
    [HttpGet]
    [Route("{userId}/friends")]  
    public virtual ActionResult<List<UserProfileResponseDto>> GetUsersUserIdFriends([FromRoute][Required] uint userId, [FromQuery][Required()] uint? limit, [FromQuery] uint? nextCursor)
    {
        return Ok(new List<UserProfileResponseDto>() { new UserProfileResponseDto() });
    }

    /// <summary>
    /// GetAllUserPosts
    /// </summary>
    /// <remarks>Get all user's posts using pagination.</remarks>    
    [HttpGet]
    [Route("{userId}/posts")]
    public virtual ActionResult<List<PostResponseDto>> GetUsersUserIdPosts([FromRoute][Required] uint userId, [FromQuery]uint? limit, [FromQuery]uint? currCursor)
    {
        return Ok(new List<PostResponseDto>() { new PostResponseDto()});
    }

    /// <summary>
    /// GetUserProfile
    /// </summary>
    /// <remarks>Get user's profile.</remarks>           
    [HttpGet]
    [Route("{userId}/profile")]
    public virtual ActionResult<UserProfileResponseDto> GetUsersUserIdProfile([FromRoute][Required]uint userId)
    {
        return Ok(new UserProfileResponseDto());
    }

    /// <summary>
    /// ChangeUserActivityFields
    /// </summary>
    /// <remarks>Makes user's account deactivated (for account owner or admin).</remarks>    
    [HttpPatch]
    [Route("{userId}/activity")]
    public virtual ActionResult<UserActivityResponseDto> PatchUsersUserId([FromRoute][Required]uint userId, [FromBody][Required] UserActivityRequestDto userActivityRequestDto)
    {
        return Ok(new UserActivityResponseDto());
    }

    /// <summary>
    /// ChangeUserLogin
    /// </summary>
    /// <remarks>Change Login.</remarks>        
    [HttpPatch]
    [Route("{userId}/login")]
    public virtual ActionResult<UserLoginResponseDto> PatchUsersUserIdLogin([FromRoute][Required]uint userId, [FromBody][Required] UserChangeLoginRequestDto userChangeLoginRequestDto)
    {
        return Ok(new UserLoginResponseDto());
    }

    /// <summary>
    /// ChangeUserPassword
    /// </summary>
    /// <remarks>Change Password.</remarks>        
    [HttpPatch]
    [Route("{userId}/password")]
    public virtual ActionResult<UserPasswordResponseDto> PatchUsersUserIdPassword([FromRoute][Required] uint userId, [FromBody][Required] UserChangeLoginRequestDto userChangeLoginRequestDto)
    {
        return Ok(new UserPasswordResponseDto());
    }

    /// <summary>
    /// ChangeUserEmail
    /// </summary>
    /// <remarks>Change user email.</remarks>        
    [HttpPatch]
    [Route("{userId}/email")]
    public virtual ActionResult<UserEmailResponseDto> PatchUsersUserIdProfile([FromRoute][Required] uint userId, [FromBody][Required] UserEmailRequestDto userLoginRequestDto)
    {        
        return Ok(new UserEmailResponseDto());
    }

    /// <summary>
    /// ChangeUserProfile
    /// </summary>
    /// <remarks>Change user profile(status, sex).</remarks>        
    [HttpPatch]
    [Route("{userId}/profile")]
    public virtual ActionResult<UserProfileResponseDto> PatchUsersUserIdProfile([FromRoute][Required]uint userId, [FromBody][Required] UserProfileRequestDto userLoginRequestDto)
    {
        return Ok(new UserProfileResponseDto());
    }


    /// <summary>
    /// CreateUserPost
    /// </summary>
    /// <remarks>Create user's post.</remarks>    
    [HttpPost]
    [Route("{userId}/posts")]
    public virtual ActionResult<PostResponseDto> PostUsersUserIdPosts([FromRoute][Required]uint userId, [FromBody][Required] PostRequestDto postRequestDto)
    {
        return Ok(new PostResponseDto());
    }
}