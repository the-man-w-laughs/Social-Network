using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository; // temp    
    public UsersController(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    /// <summary>
    /// GetAllUsers
    /// </summary>
    /// <remarks>Returns all users using pagination.</remarks>
    [HttpGet]        
    public virtual ActionResult<List<UserProfileResponseDto>> GetUsers(
        [FromQuery, Required] uint limit,
        [FromQuery, Required] uint currCursor)
    {
        var userProfiles = new List<UserProfile>
        {
            new() { UserSex = "helicopter", UserName = "Zhanna" },
            new() { UserSex = "Mig - 29", UserName = "Polina" }
        };
        
        return Ok(userProfiles.Select(user => _mapper.Map<UserProfileResponseDto>(user)));        
    }

    /// <summary>
    /// GetAllUserChats
    /// </summary>
    /// <remarks>Get all users chats using pagination (for account owner).</remarks>
    [HttpGet]
    [Route("{userId}/chats")]        
    public virtual ActionResult<List<ChatResponseDto>> GetUsersUserIdChats(
        [FromQuery, Required] uint limit,
        [FromQuery, Required] uint nextCursor)
    {
        var userChats = new List<Chat>
        {
            new() { Id = 200, Name = "TestChatName", CreatedAt = DateTime.Now },
            new() { Id = 201, Name = "TestChatName", CreatedAt = DateTime.Now.AddDays(-1) }
        };
        
        return Ok(userChats.Select(uc => _mapper.Map<ChatResponseDto>(uc)));
    }

    /// <summary>
    /// GetAllUserCommunities
    /// </summary>
    /// <remarks>Get user's communities using pagination.</remarks>
    [HttpGet]
    [Route("{userId}/communities")]
    public virtual ActionResult<List<CommunityResponseDto>> GetUsersUserIdCommunities(
        [FromRoute, Required] uint userId,
        [FromQuery, Required] uint limit,
        [FromQuery, Required] uint nextCursor)
    {
        var communities = new List<Community>
        {
            new() { Name = "TestCommunity1", Description = "TestCommunityDescription1", IsPrivate = false },
            new() { Name = "TestCommunity2", Description = "TestCommunityDescription2", IsPrivate = true }
        };
        
        return Ok(communities.Select(c => _mapper.Map<CommunityResponseDto>(c)));
    }

    /// <summary>
    /// GetAllUserFriends
    /// </summary>
    /// <remarks>Get all user's friends using pagination.</remarks>
    [HttpGet]
    [Route("{userId}/friends")]  
    public virtual ActionResult<List<UserProfileResponseDto>> GetUsersUserIdFriends(
        [FromRoute, Required] uint userId,
        [FromQuery, Required] uint limit,
        [FromQuery, Required] uint nextCursor)
    {
        var userProfiles = new List<UserProfile>
        {
            new() { UserSex = "helicopter", UserName = "Zhanna" },
            new() { UserSex = "Mig - 29", UserName = "Polina" }
        };
        
        return Ok(userProfiles.Select(user => _mapper.Map<UserProfileResponseDto>(user))); 
    }

    /// <summary>
    /// GetAllUserPosts
    /// </summary>
    /// <remarks>Get all user's posts using pagination.</remarks>
    [HttpGet]
    [Route("{userId}/posts")]
    public virtual ActionResult<List<PostResponseDto>> GetUsersUserIdPosts(
        [FromRoute, Required] uint userId,
        [FromQuery, Required] uint limit,
        [FromQuery, Required] uint currCursor)
    {
        var userPosts = new List<Post>
        {
            new() { Id = 200, Content = "TestPostDescription1", CreatedAt = DateTime.Now },
            new() { Id = 201, Content = "TestPostDescription2", CreatedAt = DateTime.Now.AddDays(-1) }
        };
        
        return Ok(userPosts.Select(up => _mapper.Map<PostResponseDto>(up)));
    }

    /// <summary>
    /// GetUserProfile
    /// </summary>
    /// <remarks>Get user's profile.</remarks>
    [HttpGet]
    [Route("{userId}/profile")]
    public virtual ActionResult<UserProfileResponseDto> GetUsersUserIdProfile([FromRoute, Required] uint userId)
    {
        var userProfile = new UserProfile { UserSex = "helicopter", UserName = "Zhanna" };
        
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
        var users = await _userRepository.GetAllAsync(user => user.Id == userId);
        if (users.Count == 0) return NotFound("User with this ID isn't found");

        var existingUser = users.First();
        _mapper.Map(userChangeLoginRequestDto, existingUser);

        _userRepository.Update(existingUser);

        await _userRepository.SaveAsync();

        return Ok(_mapper.Map<UserLoginResponseDto>(existingUser));
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
    public virtual ActionResult<PostResponseDto> PostUsersUserIdPosts(
        [FromRoute, Required] uint userId,
        [FromBody, Required] PostRequestDto postRequestDto)
    {
        var post = new Post { Id = 200, Content = "TestPostDescription1", CreatedAt = DateTime.Now };
        
        return Ok(_mapper.Map<PostResponseDto>(post));
    }

    /// <summary>
    /// CreateUserMedia
    /// </summary>
    /// <remarks>Create user media.</remarks>    
    [HttpPost]
    [Route("{userId}/medias")]
    public virtual ActionResult<List<MediaResponseDto>> PostUsersUserIdMedias([FromRoute][Required] uint userId,List<IFormFile> files)
    {
        if (files == null)
        {
            return BadRequest();
        }

        string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadedFiles");
        
        foreach(var file in files)
        {
            string filePath = Path.Combine(directoryPath, file.FileName);
            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }

        return Ok("Uploaded Successful");
    }
}