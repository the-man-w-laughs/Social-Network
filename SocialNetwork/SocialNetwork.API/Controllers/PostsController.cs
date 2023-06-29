using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Middlewares;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Comments.Request;
using SocialNetwork.BLL.DTO.Comments.Response;
using SocialNetwork.BLL.DTO.Posts.Request;
using SocialNetwork.BLL.DTO.Posts.Response;
using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IPostService _postService;
    private readonly ICommentService _commentService;

    public PostsController(IMapper mapper, IPostService postService, ICommentService commentService)
    {
        _mapper = mapper;
        _postService = postService;
        _commentService = commentService;
    }

    /// <summary>
    /// Repost
    /// </summary>
    /// <remarks>Change post.</remarks>
    [HttpPost]
    [Route("{postId}")]
    public virtual ActionResult<PostResponseDto> PostPostsPostId(
        [FromRoute, Required] uint postId,
        [FromBody, Required] PostRequestDto postRequestDto)
    {
        var post = new Post { Id = 200, Content = "TestPostDescription", CreatedAt = DateTime.Now };
        
        return Ok(_mapper.Map<PostResponseDto>(post));
    }

    /// <summary>
    /// ChangePost
    /// </summary>
    /// <remarks>Change post.</remarks>
    [HttpPut]
    [Route("{postId}")]       
    public virtual ActionResult<PostResponseDto> PutPostsPostId(
        [FromRoute, Required]uint postId,
        [FromBody, Required] PostRequestDto postRequestDto)
    {
        var post = new Post { Id = 200, Content = "TestPostDescription", CreatedAt = DateTime.Now };
        
        return Ok(_mapper.Map<PostResponseDto>(post));
    }

    /// <summary>
    /// DeletePost
    /// </summary>
    /// <remarks>Delete post.</remarks>
    [HttpDelete]
    [Route("{postId}")]
    public virtual ActionResult<PostResponseDto> DeletePostsPostId([FromRoute, Required] uint postId)
    {
        var post = new Post { Id = 200, Content = "TestPostDescription", CreatedAt = DateTime.Now };
        
        return Ok(_mapper.Map<PostResponseDto>(post));
    }

    /// <summary>
    /// LikePost
    /// </summary>
    /// <remarks>Like post.</remarks>
    [HttpPost]
    [Route("{postId}/likes")]
    public virtual ActionResult<PostLikeResponseDto> PostPostsPostIdLikes([FromRoute, Required] uint postId)
    {
        var postLike = new PostLike { Id = 200, CreatedAt = DateTime.Now };
        
        return Ok(_mapper.Map<PostLikeResponseDto>(postLike));
    }

    /// <summary>
    /// GetAllPostLikes
    /// </summary>
    /// <remarks>Get all post likes using pagination.</remarks>
    [HttpGet]
    [Route("{postId}/likes")]
    public virtual ActionResult<List<PostLikeResponseDto>> GetPostsPostIdLikes(
        [FromRoute, Required] string postId,
        [FromQuery, Required] uint limit,
        [FromQuery, Required] uint currCursor)
    {
        var postLikes = new List<PostLike>
        {
            new() { Id = 200, CreatedAt = DateTime.Now },
            new() { Id = 201, CreatedAt = DateTime.Now.AddDays(-1) }
        };
        
        return Ok(postLikes.Select(pl => _mapper.Map<PostLikeResponseDto>(pl)));
    }

    /// <summary>
    /// UnlikePost
    /// </summary>
    /// <remarks>Unlike post (for like owner).</remarks>
    [HttpDelete]
    [Route("{postId}/likes")]
    public virtual ActionResult<PostLikeResponseDto> DeletePostsPostIdLikes([FromRoute, Required] uint postId)
    {
        var postLike = new PostLike { Id = 200, CreatedAt = DateTime.Now };
        
        return Ok(_mapper.Map<PostLikeResponseDto>(postLike));
    }

    /// <summary>
    /// GetAllPostComments
    /// </summary>
    /// <remarks>Get all post comments using pagination.</remarks>
    [HttpGet]
    [Route("{postId}/comments")]
    public virtual ActionResult<List<CommentResponseDto>> GetPostsPostIdComments(
        [FromRoute, Required] uint postId,
        [FromQuery, Required] uint limit,
        [FromQuery, Required] uint currCursor)
    {
        var comments = new List<Comment>
        {
            new() { Id = 200, Content = "TestComment1", CreatedAt = DateTime.Now },
            new() { Id = 201, Content = "TestComment2", CreatedAt = DateTime.Now.AddDays(-1) }
        };

        return Ok(_mapper.Map<CommentResponseDto>(comments));
    }
}