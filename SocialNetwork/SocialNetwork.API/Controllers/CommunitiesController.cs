using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]    
public class CommunitiesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMediaService _mediaService;
    private readonly ICommunityService _communityService;
    private readonly IFileService _fileService;

    public CommunitiesController(
        IMapper mapper,
        ICommunityService communityService,
        IMediaService mediaService,
        IFileService fileService,
        IWebHostEnvironment webHostEnvironment)
    {
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
        _mediaService = mediaService;
        _fileService = fileService;
        _communityService = communityService;
    }

    /// <summary>
    /// CreateCommunity.
    /// </summary>
    /// <remarks>Create a community.</remarks>
    /// <param name="communityRequestDto">The information of the community to create.</param>    
    /// <response code="200">Returns the created community.</response>
    [HttpPost]
    [Authorize(Roles = "Admin, User")]
    [ProducesResponseType(typeof(CommunityResponseDto), StatusCodes.Status200OK)]
    public virtual async Task<ActionResult<CommunityResponseDto>> PostCommunities(
        [FromBody, Required] CommunityRequestDto communityRequestDto)
    {
        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var userId = uint.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        var newCommunity = new Community()
        {
            CreatedAt = DateTime.Now,
            Name = communityRequestDto.Name,            
            IsPrivate = communityRequestDto.IsPrivate,                        
        };
        
        var addedCommunity  = await _communityService.AddCommunity(newCommunity);

        var communityOwner = new CommunityMember()
        {
            CommunityId = addedCommunity.Id,
            CreatedAt = DateTime.Now,
            TypeId = CommunityMemberType.Owner,
            UserId = userId
        };

        await _communityService.AddCommunityMember(communityOwner);
        
        return Ok(_mapper.Map<CommunityResponseDto>(addedCommunity));
    }

    /// <summary>
    /// GetAllCommunities
    /// </summary>
    /// <remarks>Get all communities using pagination.</remarks>
    /// <param name="limit">The maximum number of communities to retrieve.</param>
    /// <param name="currCursor">The current cursor position for pagination.</param>    
    /// <response code="200">Returns the list of communities.</response>
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    [ProducesResponseType(typeof(List<CommunityResponseDto>), StatusCodes.Status200OK)]
    public virtual async Task<ActionResult<List<CommunityResponseDto>>> GetCommunities(
        [FromQuery, Required] int limit,
        [FromQuery, Required] int currCursor)
    {
        var communities = await _communityService.GetCommunities(limit, currCursor);
        
        return Ok(communities.Select(c => _mapper.Map<CommunityResponseDto>(c)));
    }

    /// <summary>
    /// ChangeCommunityInfo
    /// </summary>
    /// <remarks>Change community info (for community admins, admins).</remarks>        
    /// <param name="communityId">The ID of the community to update.</param>
    /// <param name="communityPatchRequestDto">The data for updating the community.</param>    
    /// <response code="200">Returns the updated community.</response>
    /// <response code="400">If the community with the specified ID doesn't exist or an error occurs during the update.</response>
    /// <response code="403">If the user is not authorized to update the community.</response>
    [HttpPatch]
    [Authorize(Roles = "Admin, User")]
    [Route("{communityId}")]
    [ProducesResponseType(typeof(CommunityResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async virtual Task<ActionResult<CommunityResponseDto>> PatchCommunitiesCommunityId(
        [FromRoute, Required] uint communityId,
        [FromBody, Required] CommunityPatchRequestDto communityPatchRequestDto)
    {
        var community = await _communityService.GetCommunityById(communityId);

        var isCommunityExists = community != null;

        if (!isCommunityExists)
        {
            return BadRequest("Community with this id doesnt exist");
        }

        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var userRole = isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;

        var userId = int.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        if (userRole == UserType.User.ToString())
        {
            var communityOwner = await _communityService.GetCommunityOwner(communityId);
            var isUserCommunityOwner = userId == communityOwner.UserId;
            if (!isUserCommunityOwner)
            {
                return Forbid("You are not Community Owner");
            }
        }
        try
        {
            var updatedCommunity = await _communityService.ChangeCommunity(communityId, communityPatchRequestDto);

            return Ok(updatedCommunity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// DeleteCommunity
    /// </summary>
    /// <remarks>Delete community (for community admins, admins).</remarks>          
    /// <param name="communityId">The ID of the community to delete.</param>   
    /// <response code="200">Returns the deleted community.</response>
    /// <response code="400">If the community with the specified ID doesn't exist.</response>
    /// <response code="403">If the user is not authorized to delete the community.</response>
    [HttpDelete]
    [Authorize(Roles = "Admin, User")]
    [Route("{communityId}")]
    [ProducesResponseType(typeof(CommunityResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<CommunityResponseDto>> DeleteCommunitiesCommunityId([FromRoute, Required] uint communityId)
    {
        var community = await _communityService.GetCommunityById(communityId);

        var isCommunityExists = community != null;

        if (!isCommunityExists)
        {
            return BadRequest("Community with this id doesnt exist");
        }
        
        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var userRole = isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;

        var userId = int.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        if (userRole == UserType.User.ToString())
        {
            var communityOwner = await _communityService.GetCommunityOwner(communityId);
            var isUserCommunityOwner = userId == communityOwner.UserId;
            if (!isUserCommunityOwner)
            {
                return Forbid("You are not Community Owner");
            }
        }

        var deletedCommunity = await _communityService.DeleteCommunity(communityId);
        
        return Ok(_mapper.Map<CommunityResponseDto>(deletedCommunity));
    }

    /// <summary>
    /// CreateCommunityPost
    /// </summary>
    /// <remarks>Create community post (depending on the isPrivate field, it could be available for community members or for everybody).</remarks>         
    /// <param name="communityId">The ID of the community where the post will be created.</param>
    /// <param name="communityRequestDto">The post data.</param>   
    /// <response code="200">Returns the created post.</response>
    /// <response code="400">If the community with the specified ID doesn't exist.</response>
    /// <response code="403">If the user is not authorized to create a post in the community.</response>
    [HttpPost]
    [Authorize(Roles = "Admin, User")]
    [Route("{communityId}/posts")]
    [ProducesResponseType(typeof(PostResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<PostResponseDto>> PostCommunitiesCommunityIdPosts(
        [FromRoute, Required] uint communityId,
        [FromBody, Required] PostRequestDto communityRequestDto)
    {
        var community = await _communityService.GetCommunityById(communityId);

        var isCommunityExisted = community != null;

        if (!isCommunityExisted)
        {
            return BadRequest("Community doesn't exist");
        }
        
        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var userRole = isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;

        var userId = uint.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        if (userRole == UserType.User.ToString())
        {
            if (community!.IsPrivate)
            {
                var isUserCommunityMember = await _communityService.IsUserCommunityMember(communityId, userId);
                if (!isUserCommunityMember)
                {
                    return Forbid("Community is Private, you are not a member");
                }
            }
        }

        var post = new Post()
        {
            Content = communityRequestDto.Content,
            CreatedAt = DateTime.Now
        };

        var addedPost = await _communityService.AddCommunityPost(communityId, post, userId);
        
        return Ok(_mapper.Map<PostResponseDto>(addedPost));
    }

    /// <summary>
    /// GetAllCommunityPosts
    /// </summary>
    /// <remarks>Get all community posts using pagination.</remarks>    
    /// <param name="communityId">The ID of the community.</param>
    /// <param name="limit">The maximum number of posts to retrieve.</param>
    /// <param name="currCursor">The cursor for pagination.</param>    
    /// <response code="200">Returns the list of community posts.</response>
    /// <response code="400">If the community with the specified ID doesn't exist.</response>
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    [Route("{communityId}/posts")]
    [ProducesResponseType(typeof(List<CommunityPostResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<List<CommunityPostResponseDto>>> GetCommunitiesPosts(
        [FromRoute, Required] uint communityId,
        [FromQuery, Required] int limit,
        [FromQuery, Required] int currCursor)
    {
        var communityPosts = await _communityService.GetCommunityPosts(communityId, limit, currCursor);
        
        return Ok(communityPosts.Select(cp=>_mapper.Map<CommunityPostResponseDto>(cp)));
    }

    /// <summary>
    /// CreateCommunityMedia
    /// </summary>
    /// <remarks>Create community media.</remarks>    
    /// <param name="communityId">The ID of the community.</param>
    /// <param name="files">The media files to upload.</param>    
    /// <response code="200">Indicates that the upload was successful.</response>
    /// <response code="400">If no files are provided.</response>
    [HttpPost]
    [Route("{communityId}/medias")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public virtual ActionResult<List<MediaResponseDto>> PostcommunityscommunityIdMedias([FromRoute][Required] uint communityId, [Required] List<IFormFile> files)
    {
        if (files == null)
        {
            return BadRequest();
        }
        
        string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadedFiles");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        foreach (var file in files)
        {
            string filePath = Path.Combine(directoryPath, file.FileName);
            string modifiedFilePath = _fileService.ModifyFilePath(filePath);
            using (var stream = new FileStream(modifiedFilePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            _mediaService.AddCommunityMedia(modifiedFilePath, communityId, file.FileName);
        }

        return Ok("Uploaded Successful");
    }

    /// <summary>
    /// GetAllCommunityMedias
    /// </summary>
    /// <remarks>Get all community's media using pagination. (only for community or admin)</remarks>
    /// <param name="communityId">The ID of the community.</param>
    /// <param name="limit">The maximum number of media items to retrieve.</param>
    /// <param name="currCursor">The cursor for pagination.</param>   
    /// <response code="200">Returns the list of community media.</response>
    [HttpGet]
    [Route("{communityId}/medias")]
    [ProducesResponseType(typeof(List<MediaResponseDto>), StatusCodes.Status200OK)]
    public async virtual Task<ActionResult<List<MediaResponseDto>>> GetcommunityscommunityIdMedias(
        [FromRoute, Required] uint communityId,
        [FromQuery, Required] int limit,
        [FromQuery] int currCursor)
    {
        var result = await _mediaService.GetCommunityMediaList(communityId, limit, currCursor);
        return Ok(result);
    }
}