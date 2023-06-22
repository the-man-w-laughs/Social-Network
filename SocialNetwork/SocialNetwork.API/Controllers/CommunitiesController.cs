using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
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
    /// CreateCommunity
    /// </summary>
    /// <remarks>Create community.</remarks>
    [HttpPost]
    [Authorize(Roles = "Admin, User")]
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
            Description = communityRequestDto.Description,
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
    [HttpGet]      
    [Authorize(Roles = "Admin, User")]
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
    [HttpPut]
    [Authorize(Roles = "Admin, User")]
    [Route("{communityId}")]
    public virtual ActionResult<CommunityResponseDto> PutCommunitiesCommunityId(
        [FromRoute, Required] uint communityId,
        [FromBody, Required] CommunityRequestDto communityRequestDto)
    {
        var community = new Community { Name = "TestCommunity", Description = "TestCommunityDescription", IsPrivate = false };
        
        return Ok(_mapper.Map<CommunityResponseDto>(community));
    }

    /// <summary>
    /// DeleteCommunity
    /// </summary>
    /// <remarks>Delete community (for community admins, admins).</remarks>          
    [HttpDelete]
    [Authorize(Roles = "Admin, User")]
    [Route("{communityId}")]
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
    /// <remarks>Create community post (depending on isPrivate field it could be available for community members or for everybody).</remarks>         
    [HttpPost]
    [Authorize(Roles = "Admin, User")]
    [Route("{communityId}/posts")]
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
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    [Route("{communityId}/posts")]
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
    [HttpPost]
    [Route("{communityId}/medias")]
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
    /// GetAllcommunityMedias
    /// </summary>
    /// <remarks>Get all community's posts using pagination. (only for community or admin)</remarks>
    [HttpGet]
    [Route("{communityId}/medias")]
    public async virtual Task<ActionResult<List<CommunityMediaOwnerResponseDto>>> GetcommunityscommunityIdMedias(
        [FromRoute, Required] uint communityId,
        [FromQuery, Required] int limit,
        [FromQuery] int currCursor)
    {
        var result = await _mediaService.GetCommunityMediaList(communityId, limit, currCursor);
        return Ok(result);
    }
}