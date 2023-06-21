using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTO.Comments.Request;
using SocialNetwork.BLL.DTO.Comments.Response;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ControllersController : ControllerBase
{
    /// <summary>
    /// ReplyComment
    /// </summary>
    /// <remarks>Reply comment (for comment owner).</remarks>          
    [HttpPost]
    [Route("{commentId}")]
    public virtual ActionResult<CommentResponseDto> PostCommentsCommentId(
        [FromRoute, Required] uint postId,
        [FromRoute, Required] uint commentId,
        [FromBody, Required] CommentRequestDto commentRequestDto)
    {
        return new CommentResponseDto();
    }

    /// <summary>
    /// ChangePostComment
    /// </summary>
    /// <remarks>Change post comment (for comment owner).</remarks>          
    [HttpPut]
    [Route("{commentId}")]
    public virtual ActionResult<CommentResponseDto> PutCommentsCommentId(
        [FromRoute, Required] uint postId,
        [FromRoute, Required] uint commentId,
        [FromBody, Required] CommentRequestDto commentRequestDto)
    {
        return new CommentResponseDto();
    }

    /// <summary>
    /// DeletePostComment
    /// </summary>
    /// <remarks>Delete post comment (for comment owner).</remarks>                   
    [HttpDelete]
    [Route("{commentId}")]
    public virtual ActionResult<CommentResponseDto> DeleteCommentsCommentId([FromRoute, Required] uint commentId)
    {
        return new CommentResponseDto();
    }

    /// <summary>
    /// LikePostComment
    /// </summary>
    /// <remarks>Like post comment.</remarks>                 
    [HttpPost]
    [Route("{commentId}/likes")]
    public virtual ActionResult<CommentLikeResponseDto> PostCommentsCommentIdLikes([FromRoute, Required] uint commentId)
    {
        return new CommentLikeResponseDto();
    }

    /// <summary>
    /// GetAllPostCommentLikes
    /// </summary>
    /// <remarks>Get all post comment likes using pagination.</remarks>            
    [HttpGet]
    [Route("{commentId}/likes")]
    public virtual ActionResult<List<CommentLikeResponseDto>> GetCommentsCommentIdLikes(
        [FromRoute, Required] uint commentId,
        [FromQuery, Required] uint limit, 
        [FromQuery, Required] uint currCursor)
    {
        return new List<CommentLikeResponseDto>() { new CommentLikeResponseDto() };
    }

    /// <summary>
    /// UnlikePostComment
    /// </summary>
    /// <remarks>Unlike post comment (for comment owner).</remarks>                 
    [HttpDelete]
    [Route("{commentId}/likes")]
    public virtual ActionResult<CommentLikeResponseDto> DeleteComments([FromRoute, Required] uint commentId)
    {
        return new CommentLikeResponseDto();
    }
}