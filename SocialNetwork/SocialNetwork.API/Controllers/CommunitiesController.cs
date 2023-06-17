using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]    
public class CommunitiesController : ControllerBase
{ 
    /// <summary>
    /// DeleteCommunity
    /// </summary>
    /// <remarks>Delete community (for community admins, admins).</remarks>
    /// <param name="communityId"></param>        
    [HttpDelete]
    [Route("{communityId}")]
    public virtual IActionResult DeleteCommunitiesCommunityId([FromRoute][Required]string communityId)
    {
        return Ok($"DeleteCommunity");
    }

    /// <summary>
    /// GetAllCommunities
    /// </summary>
    /// <remarks>Get all communities using pagination.</remarks>
    /// <param name="limit"></param>
    /// <param name="currCursor"></param>
    [HttpGet]              
    public virtual IActionResult GetCommunities([FromQuery][Required()]decimal? limit, [FromQuery]decimal? currCursor)
    {
        return Ok($"GetAllCommunities");
    }

    /// <summary>
    /// GetAllCommunityPosts
    /// </summary>
    /// <remarks>Get all community posts using pagination.</remarks>
    /// <param name="communityId"></param>
    /// <param name="limit"></param>
    /// <param name="currCursor"></param>
    [HttpGet]
    [Route("{communityId}/posts")]
    public virtual IActionResult GetCommunitiesPosts([FromRoute][Required]string communityId, [FromQuery]decimal? limit, [FromQuery]decimal? currCursor)
    {
        return Ok($"GetAllCommunityPosts");
    }

    /// <summary>
    /// ChangeCommunityInfo
    /// </summary>
    /// <remarks>Change community info (for community admins, admins).</remarks>
    /// <param name="communityId"></param>        
    [HttpPatch]
    [Route("{communityId}")]
    public virtual IActionResult PatchCommunitiesCommunityId([FromRoute][Required]string communityId)
    {
        return Ok($"ChangeCommunityInfo");
    }

    /// <summary>
    /// CreateCommunity
    /// </summary>
    /// <remarks>Create community.</remarks>        
    [HttpPost]        
    public virtual IActionResult PostCommunities()
    {
        return Ok($"CreateCommunity");
    }

    /// <summary>
    /// CreateCommunityPost
    /// </summary>
    /// <remarks>Create community post (depending on isPrivate field it could be available for community members or for everybody).</remarks>
    /// <param name="communityId"></param>        
    [HttpPost]
    [Route("{communityId}/posts")]        
    public virtual IActionResult PostCommunitiesCommunityIdPosts([FromRoute][Required]string communityId)
    {
        return Ok($"CreateCommunityPost");
    }
}