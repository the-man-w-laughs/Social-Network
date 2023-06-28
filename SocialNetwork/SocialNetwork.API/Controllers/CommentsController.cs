using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Middlewares;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Comments.Request;
using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.DAL.Contracts.Comments;
using SocialNetwork.DAL.Entities.Comments;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICommentService _commentRepository;

    public CommentsController(IMapper mapper, ICommentService commentRepository)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
    }

    /// <summary>
    /// GetPostComment
    /// </summary>
    /// <remarks>Retrieves all the comments info.</remarks>
    [HttpGet]
    [Route("{commentId}")]
    public virtual async Task<ActionResult<CommentResponseDto>> GetCommentsCommentId(
        [FromRoute, Required] uint commentId,
        [FromBody, Required] CommentPatchRequestDto commentPatchRequestDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var comment = await _commentRepository.GetComment(userId);
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
        var updatedComment = await _commentRepository.ChangeComment(userId, commentId, commentPatchRequestDto);
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
        var deletedComment = await _commentRepository.DeleteComment(userId, commentId);
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
        var commentLike = await _commentRepository.LikeComment(userId, commentId);
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
        var commentLikes = await _commentRepository.GetCommentLikes(commentId, limit, currCursor);
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
        var commentLike = await _commentRepository.UnlikeComment(userId, commentId);
        return Ok(commentLike);
    }
}