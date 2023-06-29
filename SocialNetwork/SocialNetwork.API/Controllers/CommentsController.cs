using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Middlewares;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Comments.Request;
using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Contracts.Comments;
using SocialNetwork.DAL.Entities.Comments;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICommentService _commentService;

    public CommentsController(IMapper mapper, ICommentService commentService)
    {
        _mapper = mapper;
        _commentService = commentService;
    }
    /// <summary>
    /// CommentPost
    /// </summary>
    /// <remarks>Comment post.</remarks>
    [HttpPost]    
    public virtual async Task<ActionResult<CommentResponseDto>> PostPostsPostIdComments(        
        [FromBody, Required] CommentRequestDto commentRequestDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var comment = await _commentService.AddComment(userId, commentRequestDto);

        return Ok(comment);
    }

    /// <summary>
    /// GetPostComment
    /// </summary>
    /// <remarks>Retrieves all the comments info.</remarks>
    [HttpGet]
    [Route("{commentId}")]
    public virtual async Task<ActionResult<CommentResponseDto>> GetCommentsCommentId(
        [FromRoute, Required] uint commentId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var comment = await _commentService.GetComment(userId);
        return Ok(comment);
    }

    /// <summary>
    /// ChangePostComment
    /// </summary>
    /// <remarks>Change post comment (for comment owner).</remarks>
    [HttpPatch]
    [Route("{commentId}")]
    public virtual async Task<ActionResult<CommentResponseDto>> PatchCommentsCommentId(        
        [FromRoute, Required] uint commentId,
        [FromBody, Required] CommentPatchRequestDto commentPatchRequestDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var updatedComment = await _commentService.ChangeComment(userId, commentId, commentPatchRequestDto);
        return Ok(updatedComment);
    }

    /// <summary>
    /// DeletePostComment
    /// </summary>
    /// <remarks>Delete post comment (for comment owner).</remarks>
    [HttpDelete]
    [Route("{commentId}")]
    public virtual async Task<ActionResult<CommentResponseDto>> DeleteCommentsCommentId([FromRoute, Required] uint commentId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var deletedComment = await _commentService.DeleteComment(userId, commentId);
        return Ok(deletedComment);
    }

    /// <summary>
    /// LikePostComment
    /// </summary>
    /// <remarks>Like post comment.</remarks>
    [HttpPost]
    [Route("{commentId}/likes")]
    public virtual async Task<ActionResult<CommentLikeResponseDto>> PostCommentsCommentIdLikes([FromRoute, Required] uint commentId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var commentLike = await _commentService.LikeComment(userId, commentId);
        return Ok(commentLike);
    }

    /// <summary>
    /// GetAllPostCommentLikes
    /// </summary>
    /// <remarks>Get all post comment likes using pagination.</remarks>
    [HttpGet]
    [Route("{commentId}/likes")]
    public virtual async Task<ActionResult<List<CommentLikeResponseDto>>> GetCommentsCommentIdLikes(
        [FromRoute, Required] uint commentId,
        [FromQuery, Required] int limit,
        [FromQuery] int currCursor)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var commentLikes = await _commentService.GetCommentLikes(commentId, limit, currCursor);
        return Ok(commentLikes);
    }

    /// <summary>
    /// UnlikePostComment
    /// </summary>
    /// <remarks>Unlike post comment (for comment owner).</remarks>
    [HttpDelete]
    [Route("{commentId}/likes")]
    public virtual async Task<ActionResult<CommentLikeResponseDto>> DeleteCommentsCommentIdLikes([FromRoute, Required] uint commentId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var commentLike = await _commentService.UnlikeComment(userId, commentId);
        return Ok(commentLike);
    }
}