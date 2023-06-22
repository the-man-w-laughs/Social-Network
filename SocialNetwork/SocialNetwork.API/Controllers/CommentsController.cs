using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTO.Comments.Request;
using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.DAL.Entities.Comments;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ControllersController : ControllerBase
{
    private readonly IMapper _mapper;

    public ControllersController(IMapper mapper)
    {
        _mapper = mapper;
    }

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
        var comment = new Comment { Id = 200, Content = "TestComment", CreatedAt = DateTime.Now };

        return Ok(_mapper.Map<CommentResponseDto>(comment));
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
        var comment = new Comment { Id = 200, Content = "TestComment", CreatedAt = DateTime.Now };

        return Ok(_mapper.Map<CommentResponseDto>(comment));
    }

    /// <summary>
    /// DeletePostComment
    /// </summary>
    /// <remarks>Delete post comment (for comment owner).</remarks>
    [HttpDelete]
    [Route("{commentId}")]
    public virtual ActionResult<CommentResponseDto> DeleteCommentsCommentId([FromRoute, Required] uint commentId)
    {
        var comment = new Comment { Id = 200, Content = "TestComment", CreatedAt = DateTime.Now };

        return Ok(_mapper.Map<CommentResponseDto>(comment));
    }

    /// <summary>
    /// LikePostComment
    /// </summary>
    /// <remarks>Like post comment.</remarks>
    [HttpPost]
    [Route("{commentId}/likes")]
    public virtual ActionResult<CommentLikeResponseDto> PostCommentsCommentIdLikes([FromRoute, Required] uint commentId)
    {
        var commentLike = new CommentLike { Id = 200, CreatedAt = DateTime.Now };

        return Ok(_mapper.Map<CommentLikeResponseDto>(commentLike));
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
        var commentLikes = new List<CommentLike>
        {
            new() { Id = 200, CreatedAt = DateTime.Now },
            new() { Id = 201, CreatedAt = DateTime.Now.AddDays(-1) }
        };

        return Ok(commentLikes.Select(cl => _mapper.Map<CommentLikeResponseDto>(cl)));
    }

    /// <summary>
    /// UnlikePostComment
    /// </summary>
    /// <remarks>Unlike post comment (for comment owner).</remarks>
    [HttpDelete]
    [Route("{commentId}/likes")]
    public virtual ActionResult<CommentLikeResponseDto> DeleteComments([FromRoute, Required] uint commentId)
    {
        var commentLike = new CommentLike { Id = 200, CreatedAt = DateTime.Now };

        return Ok(_mapper.Map<CommentLikeResponseDto>(commentLike));
    }
}