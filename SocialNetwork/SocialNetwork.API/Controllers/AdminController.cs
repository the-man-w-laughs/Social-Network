using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Users.Request;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class AdminController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediaService _mediaService;
    private readonly ICommunityService _communityService;

    public AdminController(IMapper mapper, IMediaService mediaService, ICommunityService communityService)
    {
        _mapper = mapper;
        _mediaService = mediaService;
        _communityService = communityService;
    }

    /// <summary>
    /// DeleteMedia
    /// </summary>
    /// <remarks>Delete media.</remarks>    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("medias/{mediaId}")]
    public async Task<ActionResult<UserResponseDto>> DeleteAdminMedias([FromRoute, Required] uint mediaId)
    {
        var deletedMedia = await _mediaService.DeleteMedia(mediaId);
        if (System.IO.File.Exists(deletedMedia.FilePath))
        {
            System.IO.File.Delete(deletedMedia.FilePath);
            return Ok(_mapper.Map<MediaLikeResponseDto>(deletedMedia));
        }
        return NotFound("No media with this id.");
    }

    /// <summary>
    /// DeleteCommunity
    /// </summary>
    /// <remarks>Delete community report.</remarks>    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("communities/{communityId}")]
    public async Task<ActionResult<UserResponseDto>> DeleteAdminCommunities([FromRoute, Required] uint communityId)
    {
        try
        {
            var deletedCommunity = await _communityService.DeleteCommunity(communityId);
            return Ok(deletedCommunity);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// DeleteUser
    /// </summary>
    /// <remarks>Delete user report.`</remarks>    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("users/{userId}")]
    public async Task<ActionResult<UserResponseDto>> DeleteAdminUsers([FromRoute, Required] uint userId)
    {
        return Ok();
    }

    /// <summary>
    /// DeletePost
    /// </summary>
    /// <remarks>Creates a new user using the provided login, email, and password.</remarks>    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("posts/postId")]
    public async Task<ActionResult<UserResponseDto>> DeleteAdminPosts([FromRoute, Required] uint postId)
    {
        return Ok();
    }

    /// <summary>
    /// DeleteComment
    /// </summary>
    /// <remarks>Creates a new user using the provided login, email, and password.</remarks>    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("comments/commentId")]
    public async Task<ActionResult<UserResponseDto>> DeleteAdminComments([FromRoute, Required] uint commentId)
    {
        return Ok();
    }
}