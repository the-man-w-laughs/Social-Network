using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Controllers
{    
    [ApiController]
    [Route("[controller]")]
    public class ControllersController : ControllerBase
    {
        /// <summary>
        /// DeletePostComment
        /// </summary>
        /// <remarks>Delete post comment (for comment owner).</remarks>        
        /// <param name="commentId"></param>        
        [HttpDelete]
        [Route("{commentId}")]
        public virtual IActionResult DeletePostsPostIdCommentsCommentId([FromRoute][Required] string commentId)
        {
            return Ok("DeletePostComment");
        }

        /// <summary>
        /// UnlikePostComment
        /// </summary>
        /// <remarks>Unlike post comment (for comment owner).</remarks>        
        /// <param name="commentId"></param>        
        [HttpDelete]
        [Route("{commentId}/likes")]
        public virtual IActionResult DeletePostsPostIdComments([FromRoute][Required] string commentId)
        {
            return Ok("UnlikePostComment");
        }

        /// <summary>
        /// GetAllPostCommentLikes
        /// </summary>
        /// <remarks>Get all post comment likes using pagination.</remarks>        
        /// <param name="commentId"></param>
        /// <param name="limit"></param>
        /// <param name="currCursor"></param>
        [HttpGet]
        [Route("{commentId}/likes")]
        public virtual IActionResult GetPostsCommentPostId([FromRoute][Required] string commentId, [FromQuery] decimal? limit, [FromQuery] decimal? currCursor)
        {
            return Ok("GetAllPostCommentLikes");
        }

        /// <summary>
        /// LikePostComment
        /// </summary>
        /// <remarks>Like post comment.</remarks>        
        /// <param name="commentId"></param>        
        [HttpPost]
        [Route("{commentId}/likes")]
        public virtual IActionResult PostPostsPostIdCommentsCommentIdLikes([FromRoute][Required] string commentId)
        {
            return Ok("LikePostComment");
        }
    }
}
