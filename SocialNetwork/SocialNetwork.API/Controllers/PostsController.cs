using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Controllers
{
    /// <summary>
    /// 
    /// </summary>

    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    { 
        /// <summary>
        /// DeletePost
        /// </summary>
        /// <remarks>Delete post.</remarks>
        /// <param name="postId"></param>        
        [HttpDelete]
        [Route("{postId}")]        
        public virtual IActionResult DeletePostsPostId([FromRoute][Required]string postId)
        {
            return Ok("DeletePost");
        }

        /// <summary>
        /// UnlikePost
        /// </summary>
        /// <remarks>Unlike post (for like owner).</remarks>
        /// <param name="postId"></param>        
        [HttpDelete]
        [Route("{postId}/likes")]        
        public virtual IActionResult DeletePostsPostIdLikes([FromRoute][Required]string postId)
        {
            return Ok("UnlikePost");
        }

        /// <summary>
        /// GetAllPostLikes
        /// </summary>
        /// <remarks>Get all post likes using pagination.</remarks>
        /// <param name="postId"></param>
        /// <param name="limit"></param>
        /// <param name="currCursor"></param>
        [HttpGet]
        [Route("{postId}/likes")]
        public virtual IActionResult GetPostsPostIdLikes([FromRoute][Required]string postId, [FromQuery]decimal? limit, [FromQuery]decimal? currCursor)
        { 
            return Ok("GetAllPostLikes");
        }

        /// <summary>
        /// GetAllPostComments
        /// </summary>
        /// <remarks>Get all post comments using pagination.</remarks>
        /// <param name="postId"></param>
        /// <param name="limit"></param>
        /// <param name="currCursor"></param>
        [HttpGet]
        [Route("{postId}/comments")]
        public virtual IActionResult GetPostsPostIdComments([FromRoute][Required]string postId, [FromQuery]decimal? limit, [FromQuery]decimal? currCursor)
        {
            return Ok("GetAllPostComments");
        }

        /// <summary>
        /// ChangePost
        /// </summary>
        /// <remarks>Change post.</remarks>
        /// <param name="postId"></param>        
        [HttpPatch]
        [Route("{postId}")]       
        public virtual IActionResult PatchPostsPostId([FromRoute][Required]string postId)
        {
            return Ok("ChangePost");
        }

        /// <summary>
        /// CommentPost
        /// </summary>
        /// <remarks>Comment post.</remarks>
        /// <param name="postId"></param>        
        [HttpPost]
        [Route("{postId}/comments")]
        public virtual IActionResult PostPostsPostIdComments([FromRoute][Required]string postId)
        {
            return Ok("CommentPost");
        }

        /// <summary>
        /// LikePost
        /// </summary>
        /// <remarks>Like post.</remarks>
        /// <param name="postId"></param>        
        [HttpPost]
        [Route("{postId}/likes")]        
        public virtual IActionResult PostPostsPostIdLikes([FromRoute][Required]string postId)
        { 
            return Ok("LikePost");
        }
    }
}
