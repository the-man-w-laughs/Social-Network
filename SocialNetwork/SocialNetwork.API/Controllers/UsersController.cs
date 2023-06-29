using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Middlewares;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMapper _mapper;
    private readonly IMediaService _mediaService;
    private readonly IFileService _fileService;
    private readonly IUserService _userService;
    private readonly IPostService _postService;

    public UsersController(
        IMapper mapper,
        IMediaService mediaService,
        IUserService userService,
        IPostService postService,
        IFileService fileService,
        IWebHostEnvironment webHostEnvironment)
    {
        _mapper = mapper;
        _userService = userService;
        _postService = postService;
        _mediaService = mediaService;
        _fileService = fileService;
        _webHostEnvironment = webHostEnvironment;
    }
    
    /// <summary>Get Users</summary>
    /// <remarks>Returns all users using pagination.</remarks>
    /// <param name="limit">The maximum number of users to retrieve.</param>
    /// <param name="nextCursor">The cursor value for pagination.</param>
    /// <response code="200">Returns a list of <see cref="UserResponseDto"/> with the details of the each user.</response>
    /// <response code="401">Returns a string message if the user is unauthorized.</response>
    [HttpGet]
    [Authorize(Roles = "User")]
    [ProducesResponseType(typeof(List<UserResponseDto>), StatusCodes.Status200OK)]
    public virtual async Task<ActionResult<List<UserResponseDto>>> GetUsers(
        [FromQuery, Required] int limit,
        [FromQuery] int nextCursor)
    {
        var usersDto = await _userService.GetUsers(limit, nextCursor);
        return Ok(usersDto);
    }

    /// <summary>Get User Chats</summary>
    /// <remarks>Get all users chats using pagination.</remarks>
    [Authorize(Roles = "User")]
    [HttpGet, Route("{userId}/chats")]
    public virtual async Task<ActionResult<List<ChatResponseDto>>> GetUsersUserIdChats(      
        [FromRoute, Required] uint userId,  
        [FromQuery, Required] int limit,
        [FromQuery] int nextCursor)
    {
        var userChatsDto = await _userService.GetUserChats(userId, limit, nextCursor);
        return Ok(userChatsDto);
    }

    /// <summary>Get User Communities</summary>
    /// <remarks>Get user's communities using pagination.</remarks>
    [Authorize(Roles = "User")]
    [HttpGet, Route("{userId}/communities")]
    public virtual async Task<ActionResult<List<CommunityResponseDto>>> GetUsersUserIdCommunities(
        [FromRoute, Required] uint userId,
        [FromQuery, Required] int limit,
        [FromQuery] int nextCursor)
    {
        var userCommunitiesDto = await _userService.GetUserCommunities(userId, limit, nextCursor);
        return Ok(userCommunitiesDto);
    }

    /// <summary>Get Managed Communities</summary>
    /// <remarks>Get user's communities where user is admin or owner using pagination.</remarks>
    [Authorize(Roles = "User")]
    [HttpGet, Route("{userId}/communities/managed")]
    public virtual async Task<ActionResult<List<CommunityResponseDto>>> GetUsersUserIdCommunitiesManaged(
        [FromRoute, Required] uint userId,
        [FromQuery, Required] int limit,
        [FromQuery] int nextCursor)
    {
        var managedCommunitiesDto = await _userService.GetUserManagedCommunities(userId, limit, nextCursor);
        return Ok(managedCommunitiesDto);
    }
        
    /// <summary>Get User Profile</summary>
    /// <remarks>Get user's profile.</remarks>
    [Authorize(Roles = "User")]
    [HttpGet, Route("{userId}/profile")]
    public virtual async Task<ActionResult<UserProfileResponseDto>> GetUsersUserIdProfile([FromRoute, Required] uint userId)
    {
        var userProfile = await _userService.GetUserProfile(userId);
        return Ok(_mapper.Map<UserProfileResponseDto>(userProfile));
    }

    /// <summary>Change Activity Of User Account</summary>
    /// <remarks>Makes user's account activated / deactivated (for account owner).</remarks>
    [Authorize(Roles = "User")]
    [HttpPut, Route("{userId}/activity")]
    public virtual ActionResult<UserActivityResponseDto> PutUsersUserId(
        [FromRoute, Required] uint userId,
        [FromBody, Required] UserActivityRequestDto userActivityRequestDto)
    {
        // TODO
        var user = new User { LastActiveAt = DateTime.Now };

        return Ok(_mapper.Map<UserActivityResponseDto>(user));
    }

    /// <summary>Change User Login</summary>
    /// <remarks>Change Login.</remarks>
    [Authorize(Roles = "User")]
    [HttpPut, Route("login")]
    public virtual async Task<ActionResult<UserResponseDto>> PutUsersUserIdLogin(
        [FromBody, Required] UserLoginRequestDto userLoginRequestDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var changedUserLoginDto = await _userService.ChangeUserLogin(userId, userLoginRequestDto);
        return Ok(changedUserLoginDto);
    }

    /// <summary>Change User Password</summary>
    /// <remarks>Change Password.</remarks>
    [Authorize(Roles = "User")]
    [HttpPut, Route("password")]
    public virtual async Task<ActionResult<UserResponseDto>> PutUsersUserIdPassword(
        [FromBody, Required] UserPasswordRequestDto userPasswordRequestDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var changeUserPasswordDto = await _userService.ChangeUserPassword(userId, userPasswordRequestDto);
        return Ok(changeUserPasswordDto);
    }

    /// <summary>Change User Email</summary>
    /// <remarks>Change user email.</remarks>
    [Authorize(Roles = "User")]
    [HttpPut, Route("email")]
    public virtual async Task<ActionResult<UserEmailResponseDto>> PutUsersUserIdProfile(
        [FromBody, Required] UserEmailRequestDto userEmailRequestDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var changedUserEmailDto = await _userService.ChangeUserEmail(userId, userEmailRequestDto);
        return Ok(_mapper.Map<UserEmailResponseDto>(changedUserEmailDto));
    }

    /// <summary>Change User Profile</summary>
    /// <remarks>Change user profile(status, sex).</remarks>
    [Authorize(Roles = "User")]
    [HttpPatch, Route("profile")]
    public virtual async Task<ActionResult<UserProfileResponseDto>> PutUsersUserIdProfile(
        [FromBody, Required] UserProfilePatchRequestDto userProfilePatchRequestDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var updatedUserProfile = await _userService.ChangeUserProfile(userId, userProfilePatchRequestDto);
        return Ok(updatedUserProfile);
    }

    /// <summary>Get User Posts</summary>
    /// <remarks>Get all user's posts using pagination.</remarks>
    [Authorize(Roles = "User")]
    [HttpGet, Route("{userId}/posts")]
    public virtual async Task<ActionResult<List<PostResponseDto>>> GetUsersUserIdPosts(
        [FromRoute, Required] uint userId,
        [FromQuery, Required] int limit,
        [FromQuery] int nextCursor)
    {
        //var userPosts = await _userService.GetUserPosts(userId, limit, nextCursor);
        //return Ok(userPosts);
        return Ok();
    }

    /// <summary>Get User Medias</summary>
    /// <remarks>Get all user's posts using pagination. (only for user or admin)</remarks>
    [Authorize(Roles = "User")]
    [HttpGet, Route("medias")]
    public virtual async Task<ActionResult<List<MediaResponseDto>>> GetUsersUserIdMedias(        
        [FromQuery, Required] int limit,
        [FromQuery] int currCursor)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var userMediasDto = await _mediaService.GetUserMediaList(userId, limit, currCursor);
        return Ok(userMediasDto);
    }

    // AddFriend
    [Authorize(Roles = "User")]
    [HttpPost, Route("{userId}/friends/{friendId}")]
    public virtual async Task<ActionResult<UserProfileResponseDto>> PostUserFriendsFriendId(
        [FromRoute, Required] uint userId,
        [FromRoute, Required] uint friendId)
    {
        var friendProfileDto = await _userService.AddFriend(userId, friendId);
        return Ok(friendProfileDto);
    }

    /// <summary>Get User Friends</summary>
    /// <remarks>Get all user's friends using pagination.</remarks>
    [Authorize(Roles = "User")]
    [HttpGet, Route("{userId}/friends")]
    public virtual async Task<ActionResult<List<UserResponseDto>>> GetUsersUserIdFriends(
        [FromRoute, Required] uint userId,
        [FromQuery, Required] int limit,
        [FromQuery] int nextCursor)
    {
        var userFriendsDto = await _userService.GetUserFriends(userId, limit, nextCursor);
        return Ok(userFriendsDto);
    }

    // DeleteFriend
    [Authorize(Roles = "User")]
    [HttpDelete, Route("{userId}/friends/{friendId}")]
    public virtual async Task<ActionResult<UserProfileResponseDto>> DeleteUserFriends(
        [FromRoute, Required] uint userId,
        [FromRoute, Required] uint friendId)
    {
        var deletedUserDto = await _userService.DeleteFriend(userId, friendId);
        return Ok(deletedUserDto);
    }

    // GetFollowers
    [Authorize(Roles = "User")]
    [HttpGet, Route("{userId}/followers")]
    public virtual async Task<ActionResult<List<UserResponseDto>>> GetUserFollowers(
        [FromRoute, Required] uint userId,
        [FromQuery, Required] int limit,
        [FromQuery] int currCursor)
    {
        var userFollowersDto = await _userService.GetUserFollowers(userId, limit, currCursor);
        return Ok(userFollowersDto);
    }

    // DeleteFollower
    [Authorize(Roles = "User")]
    [HttpDelete, Route("{userId}/followers/{followerId}")]
    public virtual async Task<ActionResult<UserProfileResponseDto>> DeleteUserFollowers(
        [FromRoute, Required] uint userId,
        [FromRoute, Required] uint followerId)
    {
        var deletedFollowerDto = await _userService.DeleteFollower(userId, followerId);
        return Ok(deletedFollowerDto);
    }

}