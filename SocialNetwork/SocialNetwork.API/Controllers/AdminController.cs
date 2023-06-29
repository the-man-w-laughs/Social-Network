using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.DTO.Users.Response;
using SocialNetwork.BLL.Exceptions;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class AdminController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediaService _mediaService;
    private readonly IAdminService _adminService;

    public AdminController(IMapper mapper, IMediaService mediaService, IAdminService adminService)
    {
        _mapper = mapper;
        _mediaService = mediaService;
        _adminService = adminService;
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
        var deletedCommunity = await _adminService.DeleteCommunity(communityId);
        return Ok(deletedCommunity);
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
        var deletedUser = await _adminService.DeleteUser(userId);
        return Ok(deletedUser);
    }

    /// <summary>
    /// DeletePost
    /// </summary>
    /// <remarks>Creates a new user using the provided login, email, and password.</remarks>    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("posts/postId")]
    public async Task<ActionResult<PostResponseDto>> DeleteAdminPosts([FromRoute, Required] uint postId)
    {
        var deletedPost = await _adminService.DeletePost(postId);
        return Ok(deletedPost);
    }

    /// <summary>
    /// DeleteComment
    /// </summary>
    /// <remarks>Creates a new user using the provided login, email, and password.</remarks>    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("comments/commentId")]
    public async Task<ActionResult<CommentResponseDto>> DeleteAdminComments([FromRoute, Required] uint commentId)
    {
        var deletedComment = await _adminService.DeleteComment(commentId);
        return Ok(deletedComment);
    }
}