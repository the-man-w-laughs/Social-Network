using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Middlewares;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CommunitiesController : ControllerBase
{
    private readonly ICommunityService _communityService;

    public CommunitiesController(ICommunityService communityService)
    {
        _communityService = communityService;
    }

    /// <summary>
    /// CreateCommunity.
    /// </summary>
    /// <remarks>Create a community.</remarks>
    /// <param name="communityRequestDto">The information of the community to create.</param>
    /// <returns>Returns the created community if successful, or a bad request if an error occurs.</returns>
    /// <response code="200">Returns the created community.</response>
    /// <response code="400">Returns a bad request if an error occurs.</response>
    [HttpPost]
    [Authorize(Roles = "User")]
    [ProducesResponseType(typeof(CommunityResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<CommunityResponseDto>> PostCommunities(
        [FromBody, Required] CommunityRequestDto communityRequestDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var addedCommunity = await _communityService.AddCommunity(communityRequestDto);
        await _communityService.AddCommunityOwner(addedCommunity.Id, userId);
        return Ok(addedCommunity);
    }

    /// <summary>
    /// GetCommunityInfo.
    /// </summary>
    /// <remarks>Gets community info.</remarks>
    /// <param name="communityId">Id of community to retrieve.</param>
    /// <returns>Returns the created community if successful, or a bad request if an error occurs.</returns>
    /// <response code="200">Returns the created community.</response>    
    /// <response code="403">If not a member of private community tries to get its info.</response>
    /// <response code="404">If there is no community with this id.</response>
    [HttpGet]
    [Route("{communityId}")]
    [Authorize(Roles = "User")]
    [ProducesResponseType(typeof(CommunityResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult<CommunityResponseDto>> GetCommunitiesCommunityId(
        [FromRoute, Required] uint communityId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var community = await _communityService.GetCommunity(userId, communityId);
        return Ok(community);
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
    /// <response code="400">An error occurs during the update.</response>
    /// <response code="403">If the user is not authorized to update the community.</response>
    /// <response code="404">If there is no community with this id.</response>
    [HttpPatch]
    [Authorize(Roles = "User")]
    [Route("{communityId}")]
    [ProducesResponseType(typeof(CommunityResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult<CommunityResponseDto>> PatchCommunitiesCommunityId(
        [FromRoute, Required] uint communityId,
        [FromBody, Required] CommunityPatchRequestDto communityPatchRequestDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var updatedCommunity =
            await _communityService.ChangeCommunity(userId, communityId, communityPatchRequestDto);
        return Ok(updatedCommunity);
    }

    /// <summary>
    /// DeleteCommunity
    /// </summary>
    /// <remarks>Delete community (for community admins, admins).</remarks>          
    /// <param name="communityId">The ID of the community to delete.</param>   
    /// <response code="200">Returns the deleted community.</response>    
    /// <response code="403">If the user is not authorized to delete the community.</response>
    /// <response code="404">If there is no community with this id.</response>
    [HttpDelete]
    [Authorize(Roles = "User")]
    [Route("{communityId}")]
    [ProducesResponseType(typeof(CommunityResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult<CommunityResponseDto>> DeleteCommunitiesCommunityId(
        [FromRoute, Required] uint communityId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var deletedCommunity = await _communityService.DeleteCommunity(userId, communityId);
        return Ok(deletedCommunity);
    }

    /// <summary>
    /// GetAllCommunityPosts
    /// </summary>
    /// <remarks>Get all community posts using pagination.</remarks>    
    /// <param name="communityId">The ID of the community.</param>
    /// <param name="limit">The maximum number of posts to retrieve.</param>
    /// <param name="currCursor">The cursor for pagination.</param>    
    /// <response code="200">Returns the list of community posts.</response>
    /// <response code="403">If the user is not authorized to get posts from the community.</response>
    /// <response code="404">If there is no community with this id.</response>
    [HttpGet]
    [Authorize(Roles = "User")]
    [Route("{communityId}/posts")]
    [ProducesResponseType(typeof(List<PostResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<List<PostResponseDto>>> GetCommunitiesCommunityIdPosts(
        [FromRoute, Required] uint communityId,
        [FromQuery, Required] int limit,
        [FromQuery] int currCursor)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var communityPosts = await _communityService
            .GetCommunityPosts(userId, communityId, limit, currCursor);
        return Ok(communityPosts);
    }

    /// <summary>
    /// GetAllCommunityMembers
    /// </summary>
    /// <remarks>Retrieve all members of community.</remarks>
    [HttpGet]
    [Route("{communityId}/members")]
    [Authorize(Roles = "User")]
    public virtual async Task<ActionResult> GetCommunitiesCommunityIdMembers(
        [FromRoute, Required] uint communityId,
        [FromQuery, Required] int limit,
        [FromQuery] int currCursor,
        [FromQuery] uint? communityMemberTypeId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var communityPosts = await _communityService
            .GetCommunityMembers(userId, communityId, communityMemberTypeId, limit, currCursor);
        return Ok(communityPosts);
    }

    /// <summary>
    /// AddCommunityMember
    /// </summary>
    /// <remarks>Add member to the community.</remarks>
    [HttpPost]
    [Route("{communityId}/members/{userIdToAdd}")]
    [Authorize(Roles = "User")]
    public virtual async Task<ActionResult<CommunityMemberResponseDto>> PostCommunitiesCommunityIdMembersMemberId(
        [FromRoute, Required] uint userIdToAdd,
        [FromRoute, Required] uint communityId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var addedMember = await _communityService.AddCommunityMember(userId, communityId, userIdToAdd);
        return Ok(addedMember);
    }

    /// <summary>
    /// ChangeCommunityMember
    /// </summary>
    /// <remarks>Change member of the community.</remarks>
    [HttpPut]
    [Route("{communityId}/members/{userIdToChange}")]
    [Authorize(Roles = "User")]
    public virtual async Task<ActionResult<CommunityMemberResponseDto>> PutCommunitiesCommunityIdMembersMemberId(
        [FromRoute, Required] uint userIdToChange,
        [FromRoute, Required] uint communityId,
        [FromBody, Required] CommunityMemberRequestDto communityMemberRequestDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var deletedMember = await _communityService
            .ChangeCommunityMember(userId, communityId, userIdToChange, communityMemberRequestDto);
        return Ok(deletedMember);
    }

    /// <summary>
    /// DeleteCommunityMember
    /// </summary>
    /// <remarks>Delete member of the community.</remarks>
    [HttpDelete]
    [Route("{communityId}/members/{userIdToDelete}")]
    [Authorize(Roles = "User")]
    public virtual async Task<ActionResult<CommunityMemberResponseDto>> DeleteCommunityMembers(
        [FromRoute, Required] uint userIdToAdd,
        [FromRoute, Required] uint communityId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var deletedMember = await _communityService.DeleteCommunityMember(userId, communityId, userIdToAdd);
        return Ok(deletedMember);
    }
}