using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
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
using SocialNetwork.BLL.Exceptions;
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
    [Authorize(Roles = "User")]
    [ProducesResponseType(typeof(CommunityResponseDto), StatusCodes.Status200OK)]
    public virtual async Task<ActionResult<CommunityResponseDto>> PostCommunities(
        [FromBody, Required] CommunityRequestDto communityRequestDto)
    {
        var userId = (uint)HttpContext.Items["UserId"]!;
        try {
            var addedCommunity = await _communityService.AddCommunity(communityRequestDto);
            await _communityService.AddCommunityMember(addedCommunity.Id, userId, CommunityMemberType.Owner);
            return Ok(_mapper.Map<CommunityResponseDto>(addedCommunity));
        }
        catch (ArgumentException ex){
            return BadRequest(ex.Message);
        }                          
    }

    /// <summary>
    /// GetAllCommunities
    /// </summary>
    /// <remarks>Get all communities using pagination.</remarks>
    /// <param name="limit">The maximum number of communities to retrieve.</param>
    /// <param name="currCursor">The current cursor position for pagination.</param>    
    /// <response code="200">Returns the list of communities.</response>
    [HttpGet]
    [Authorize(Roles = "User")]
    [ProducesResponseType(typeof(List<CommunityResponseDto>), StatusCodes.Status200OK)]
    public virtual async Task<ActionResult<List<CommunityResponseDto>>> GetCommunities(
        [FromQuery, Required] int limit,
        [FromQuery] int currCursor)
    {
        var communities = await _communityService.GetCommunities(limit, currCursor);

        return Ok(communities);
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
    [Authorize(Roles = "User")]
    [Route("{communityId}")]
    [ProducesResponseType(typeof(CommunityResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public async virtual Task<ActionResult<CommunityResponseDto>> PatchCommunitiesCommunityId(
        [FromRoute, Required] uint communityId,
        [FromBody, Required] CommunityPatchRequestDto communityPatchRequestDto)
    {
        var userId = (uint)HttpContext.Items["UserId"]!;
        try
        {
            var updatedCommunity = await _communityService.ChangeCommunity(userId, communityId, communityPatchRequestDto);
            return Ok(updatedCommunity);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (OwnershipException ex)
        {
            return Forbid(ex.Message);
        }
        catch (ArgumentException ex)
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
    [Authorize(Roles = "User")]
    [Route("{communityId}")]
    [ProducesResponseType(typeof(CommunityResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<CommunityResponseDto>> DeleteCommunitiesCommunityId([FromRoute, Required] uint communityId)
    {
        var userId = (uint)HttpContext.Items["UserId"]!;
        try
        {
            var deletedCommunity = await _communityService.DeleteCommunity(userId, communityId);

            return Ok(deletedCommunity);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (OwnershipException ex)
        {
            return Forbid(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }        
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
    [Authorize(Roles = "User")]
    [Route("{communityId}/posts")]
    [ProducesResponseType(typeof(PostResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<PostResponseDto>> PostCommunitiesCommunityIdPosts(
        [FromRoute, Required] uint communityId,
        [FromBody, Required] PostRequestDto postRequestDto)
    {
        var userId = (uint)HttpContext.Items["UserId"]!;
        try
        {
            var addedPost = await _communityService.AddCommunityPost(userId, communityId, postRequestDto);

            return Ok(addedPost);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (OwnershipException ex)
        {
            return Forbid(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
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
    [Authorize(Roles = "User")]
    [Route("{communityId}/posts")]
    [ProducesResponseType(typeof(List<CommunityPostResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<List<CommunityPostResponseDto>>> GetCommunitiesPosts(
        [FromRoute, Required] uint communityId,
        [FromQuery, Required] int limit,
        [FromQuery] int currCursor)
    {
        var userId = (uint)HttpContext.Items["UserId"]!;
        try
        {
            var communityPosts = await _communityService.GetCommunityPosts(userId,communityId, limit, currCursor);

            return Ok(communityPosts);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (OwnershipException ex)
        {
            return Forbid(ex.Message);
        }        
    }
}