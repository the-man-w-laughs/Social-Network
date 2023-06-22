using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IPostService _postService;
    public UsersController(IMapper mapper, IUserService userService, IPostService postService)
    {
        _mapper = mapper;
        _userService = userService;
        _postService = postService;
    }

    /// <summary>
    /// GetAllUsers
    /// </summary>
    /// <remarks>Returns all users using pagination.</remarks>
    [HttpGet]     
    [Authorize(Roles = "Admin, User")]
    public virtual async Task<ActionResult<List<UserResponseDto>>> GetUsers(
        [FromQuery, Required] int limit,
        [FromQuery, Required] int currCursor)
    {
        var users = await _userService.GetUsers(limit, currCursor);

        return Ok(users.Select(up=>_mapper.Map<UserResponseDto>(up)));        
    }

    /// <summary>
    /// GetAllUserChats
    /// </summary>
    /// <remarks>Get all users chats using pagination (for account owner).</remarks>
    [HttpGet]
    [Route("{userId}/chats")]        
    public virtual async Task<ActionResult<List<ChatResponseDto>>> GetUsersUserIdChats(
        [FromRoute,Required] int userId,
        [FromQuery, Required] int limit,
        [FromQuery, Required] int nextCursor)
    {
        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        var claimUserId = uint.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        var isUserAccountOwner = claimUserId == userId;

        if (!isUserAccountOwner)
        {
            return Forbid("You are not account owner");
        }
        
        var userChats = await _userService.GetUserChats(claimUserId,limit,nextCursor);
        
        return Ok(userChats.Select(uc => _mapper.Map<ChatResponseDto>(uc)));
    }

    /// <summary>
    /// GetAllUserCommunities
    /// </summary>
    /// <remarks>Get user's communities using pagination.</remarks>
    [HttpGet]
    [Route("{userId}/communities")]
    public virtual async Task<ActionResult<List<CommunityResponseDto>>> GetUsersUserIdCommunities(
        [FromRoute, Required] uint userId,
        [FromQuery, Required] int limit,
        [FromQuery, Required] int nextCursor)
    {
        var userCommunities = await _userService.GetUserCommunities(userId, limit, nextCursor);
        
        return Ok(userCommunities.Select(c => _mapper.Map<CommunityResponseDto>(c)));
    }

    /// <summary>
    /// GetAllUserFriends
    /// </summary>
    /// <remarks>Get all user's friends using pagination.</remarks>
    [HttpGet]
    [Route("{userId}/friends")]  
    public virtual async Task<ActionResult<List<UserResponseDto>>> GetUsersUserIdFriends(
        [FromRoute, Required] uint userId,
        [FromQuery, Required] int limit,
        [FromQuery, Required] int nextCursor)
    {
        var userFriends = await _userService.GetUserFriends(userId, limit, nextCursor);
        
        return Ok(userFriends.Select(user => _mapper.Map<UserResponseDto>(user))); 
    }

    /// <summary>
    /// GetAllUserPosts
    /// </summary>
    /// <remarks>Get all user's posts using pagination.</remarks>
    [HttpGet]
    [Route("{userId}/posts")]
    public virtual async Task<ActionResult<List<PostResponseDto>>> GetUsersUserIdPosts(
        [FromRoute, Required] uint userId,
        [FromQuery, Required] int limit,
        [FromQuery, Required] int currCursor)
    {
        var userPosts = await _userService.GetUserPosts(userId, limit, currCursor);
        
        return Ok(userPosts.Select(up => _mapper.Map<PostResponseDto>(up)));
    }

    /// <summary>
    /// GetUserProfile
    /// </summary>
    /// <remarks>Get user's profile.</remarks>
    [HttpGet]
    [Route("{userId}/profile")]
    public virtual async Task<ActionResult<UserProfileResponseDto>> GetUsersUserIdProfile([FromRoute, Required] uint userId)
    {
        var userProfile = await _userService.GetUserProfile(userId);
        
        return Ok(_mapper.Map<UserProfileResponseDto>(userProfile));
    }

    /// <summary>
    /// ChangeUserActivityFields
    /// </summary>
    /// <remarks>Makes user's account deactivated (for account owner or admin).</remarks>
    [HttpPut]
    [Route("{userId}/activity")]
    public virtual ActionResult<UserActivityResponseDto> PutUsersUserId(
        [FromRoute, Required] uint userId,
        [FromBody, Required] UserActivityRequestDto userActivityRequestDto)
    {
        var user = new User {LastActiveAt = DateTime.Now };
        
        return Ok(_mapper.Map<UserActivityResponseDto>(user));
    }

    /// <summary>
    /// ChangeUserLogin
    /// </summary>
    /// <remarks>Change Login.</remarks>
    [HttpPut]
    [Route("{userId}/login")]
    public virtual async Task<ActionResult<UserLoginResponseDto>> PutUsersUserIdLogin(
        [FromRoute, Required] uint userId,
        [FromBody, Required] UserChangeLoginRequestDto userChangeLoginRequestDto)
    {
        /*var users = await _userRepository.GetAllAsync(user => user.Id == userId);
        if (users.Count == 0) return NotFound("User with this ID isn't found");

        var existingUser = users.First();
        _mapper.Map(userChangeLoginRequestDto, existingUser);

        _userRepository.Update(existingUser);

        await _userRepository.SaveAsync();

        return Ok(_mapper.Map<UserLoginResponseDto>(existingUser));*/
        return Ok();
    }

    /// <summary>
    /// ChangeUserPassword
    /// </summary>
    /// <remarks>Change Password.</remarks>
    [HttpPut]
    [Route("{userId}/password")]
    public virtual ActionResult<UserPasswordResponseDto> PutUsersUserIdPassword(
        [FromRoute, Required] uint userId,
        [FromBody, Required] UserChangeLoginRequestDto userChangeLoginRequestDto)
    {
        var user = new User { PasswordHash = new byte[] { 12, 228, 123 } };
        
        return Ok(_mapper.Map<UserPasswordResponseDto>(user));
    }

    /// <summary>
    /// ChangeUserEmail
    /// </summary>
    /// <remarks>Change user email.</remarks>
    [HttpPut]
    [Route("{userId}/email")]
    public virtual ActionResult<UserEmailResponseDto> PutUsersUserIdProfile(
        [FromRoute, Required] uint userId,
        [FromBody, Required] UserEmailRequestDto userLoginRequestDto)
    {
        var user = new User { Email = "TestEmail@gmail.com" };
        
        return Ok(_mapper.Map<UserEmailResponseDto>(user));
    }

    /// <summary>
    /// ChangeUserProfile
    /// </summary>
    /// <remarks>Change user profile(status, sex).</remarks>
    [HttpPut]
    [Route("{userId}/profile")]
    public virtual ActionResult<UserProfileResponseDto> PutUsersUserIdProfile(
        [FromRoute, Required] uint userId,
        [FromBody, Required] UserProfileRequestDto userLoginRequestDto)
    {
        var userProfile = new UserProfile { UserSex = "helicopter", UserName = "Zhanna" };
        
        return Ok(_mapper.Map<UserProfileResponseDto>(userProfile));
    }
    
    /// <summary>
    /// CreateUserPost
    /// </summary>
    /// <remarks>Create user's post.</remarks>
    [HttpPost]
    [Route("{userId}/posts")]
    public virtual async Task<ActionResult<UserProfilePostResponseDto>> PostUsersUserIdPosts(
        [FromRoute, Required] uint userId,
        [FromBody, Required] PostRequestDto postRequestDto)
    {
        var newUserPost = new Post
        {
            Content = postRequestDto.Content,
            CreatedAt = DateTime.Now
        };

        var userProfilePost = await _postService.CreateUserProfilePost(userId,newUserPost);
        
        return Ok(_mapper.Map<UserProfilePostResponseDto>(userProfilePost));
    }
}