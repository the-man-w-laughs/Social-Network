using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.BLL.DTO.Communities.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.BLL.DTO.Users.Response;

namespace SocialNetwork.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController, Route("[controller]")]
public sealed class AdminController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAdminService _adminService;

    public AdminController(IMapper mapper, IAdminService adminService)
    {
        _mapper = mapper;
        _adminService = adminService;
    }

    /// <summary>Delete Media</summary>
    /// <remarks>Delete media by ID.</remarks>
    /// <param name="mediaId">The ID of the media to delete</param>
    /// <response code="200">Returns a <see cref="MediaResponseDto"/> with the details of the deleted media.</response>
    /// <response code="401">Returns a string message if the user unauthorized or not admin.</response>
    /// <response code="404">Returns a string message if the media not founded.</response>
    [HttpDelete, Route("medias/{mediaId}")]
    [ProducesResponseType(typeof(MediaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MediaResponseDto>> DeleteAdminMedias([FromRoute, Required] uint mediaId)
    {
        var deletedMedia = await _adminService.DeleteMedia(mediaId);
        return Ok(_mapper.Map<MediaResponseDto>(deletedMedia));
    }

    /// <summary>Delete Community</summary>
    /// <remarks>Delete community by ID.</remarks>
    /// <param name="communityId">The ID of the community to delete</param>
    /// <response code="200">Returns a <see cref="CommunityResponseDto"/> with the details of the deleted community.</response>
    /// <response code="401">Returns a string message if the user unauthorized or not admin.</response>
    /// <response code="404">Returns a string message if the community not founded.</response>
    [HttpDelete, Route("communities/{communityId}")]
    public async Task<ActionResult<CommunityResponseDto>> DeleteAdminCommunities([FromRoute, Required] uint communityId)
    {
        var deletedCommunity = await _adminService.DeleteCommunity(communityId);
        return Ok(deletedCommunity);
    }

    /// <summary>Delete User</summary>
    /// <remarks>Delete user by ID.</remarks>
    /// <param name="userId">The ID of the user to delete</param>
    /// <response code="200">Returns a <see cref="UserResponseDto"/> with the details of the deleted user.</response>
    /// <response code="401">Returns a string message if the user unauthorized or not admin.</response>
    /// <response code="404">Returns a string message if the user not founded.</response>
    [HttpDelete, Route("users/{userId}")]
    public async Task<ActionResult<UserResponseDto>> DeleteAdminUsers([FromRoute, Required] uint userId)
    {
        var deletedUser = await _adminService.DeleteUser(userId);
        return Ok(deletedUser);
    }

    /// <summary>Delete Post</summary>
    /// <remarks>Delete post by ID.</remarks>
    /// <param name="postId">The ID of the post to delete</param>
    /// <response code="200">Returns a <see cref="PostResponseDto"/> with the details of the deleted post.</response>
    /// <response code="401">Returns a string message if the user unauthorized or not admin.</response>
    /// <response code="404">Returns a string message if the post not founded.</response>
    [HttpDelete, Route("posts/{postId}")]
    public async Task<ActionResult<PostResponseDto>> DeleteAdminPosts([FromRoute, Required] uint postId)
    {
        var deletedPost = await _adminService.DeletePost(postId);
        return Ok(deletedPost);
    }

    /// <summary>Delete Comment</summary>
    /// <remarks>Delete comment by ID.</remarks>
    /// <param name="commentId">The ID of the comment to delete</param>
    /// <response code="200">Returns a <see cref="PostResponseDto"/> with the details of the deleted comment.</response>
    /// <response code="401">Returns a string message if the user unauthorized or not admin.</response>
    /// <response code="404">Returns a string message if the comment not founded.</response>
    [HttpDelete, Route("comments/{commentId}")]
    public async Task<ActionResult<CommentResponseDto>> DeleteAdminComments([FromRoute, Required] uint commentId)
    {
        var deletedComment = await _adminService.DeleteComment(commentId);
        return Ok(deletedComment);
    }
}