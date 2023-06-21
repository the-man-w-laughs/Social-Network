using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTO.Communities.Request;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]    
public class CommunitiesController : ControllerBase
{

    /// <summary>
    /// CreateCommunity
    /// </summary>
    /// <remarks>Create community.</remarks>        
    [HttpPost]
    public virtual ActionResult<CommunityResponseDto> PostCommunities([FromBody][Required] CommunityRequestDto communityRequestDto)
    {
        return Ok(new CommunityResponseDto());
    }

    /// <summary>
    /// GetAllCommunities
    /// </summary>
    /// <remarks>Get all communities using pagination.</remarks>    
    [HttpGet]              
    public virtual ActionResult<List<CommunityResponseDto>> GetCommunities([FromQuery][Required()] uint? limit, [FromQuery] uint? currCursor)
    {
        return new List<CommunityResponseDto>() { new CommunityResponseDto() };
    }

    /// <summary>
    /// ChangeCommunityInfo
    /// </summary>
    /// <remarks>Change community info (for community admins, admins).</remarks>        
    [HttpPut]
    [Route("{communityId}")]
    public virtual ActionResult<CommunityResponseDto> PutCommunitiesCommunityId([FromRoute][Required] uint communityId, [FromBody][Required] CommunityRequestDto communityRequestDto)
    {
        return Ok(new CommunityResponseDto());
    }

    /// <summary>
    /// DeleteCommunity
    /// </summary>
    /// <remarks>Delete community (for community admins, admins).</remarks>          
    [HttpDelete]
    [Route("{communityId}")]
    public virtual ActionResult<CommunityResponseDto> DeleteCommunitiesCommunityId([FromRoute][Required] uint communityId)
    {
        return new CommunityResponseDto();
    }

    /// <summary>
    /// CreateCommunityPost
    /// </summary>
    /// <remarks>Create community post (depending on isPrivate field it could be available for community members or for everybody).</remarks>         
    [HttpPost]
    [Route("{communityId}/posts")]
    public virtual ActionResult<PostResponseDto> PostCommunitiesCommunityIdPosts([FromRoute][Required] uint communityId, [FromBody][Required] PostRequestDto communityRequestDto)
    {
        return Ok(new PostResponseDto());
    }

    /// <summary>
    /// GetAllCommunityPosts
    /// </summary>
    /// <remarks>Get all community posts using pagination.</remarks>    
    [HttpGet]
    [Route("{communityId}/posts")]
    public virtual ActionResult<List<CommunityPostResponseDto>> GetCommunitiesPosts([FromRoute][Required] uint communityId, [FromQuery] uint? limit, [FromQuery] uint? currCursor)
    {
        return new List<CommunityPostResponseDto>() { new CommunityPostResponseDto() };
    }
}