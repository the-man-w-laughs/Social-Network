using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.DAL.Entities.Chats;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Controllers
{    
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        /// <summary>
        /// UnlikeMessage
        /// </summary>
        /// <remarks>Unlike chat message (for like owner).</remarks>
        /// <param name="messageId"></param>        
        [HttpDelete]
        [Route("{messageId}/likes")]
        public virtual IActionResult DeleteMessagesMessageIdLikes([FromRoute][Required] string messageId)
        {
            return Ok($"UnlikeMessage");
        }

        /// <summary>
        /// GetAllMessageLikes
        /// </summary>
        /// <remarks>Get all message likes using pagination (for chat members).</remarks>
        /// <param name="messageId"></param>
        /// <param name="limit"></param>
        /// <param name="currCursor"></param>
        [HttpGet]
        [Route("{messageId}/likes")]
        public virtual IActionResult GetMessagesMessageId([FromRoute][Required] string messageId, [FromQuery] decimal? limit, [FromQuery] string currCursor)
        {
            return Ok($"GetAllMessageLikes");
        }

        /// <summary>
        /// LikeMessage
        /// </summary>
        /// <remarks>Like chat message (for chat members).</remarks>
        /// <param name="messageId"></param>        
        [HttpPost]
        [Route("{messageId}/likes")]
        public virtual IActionResult PostMessagesMessageIdLikes([FromRoute][Required] string messageId)
        {
            return Ok($"LikeMessage");
        }

        /// <summary>
        /// ChangeMessage
        /// </summary>
        /// <remarks>Like chat message (for chat members).</remarks>
        /// <param name="messageId"></param>        
        [HttpPatch]
        [Route("{messageId}")]
        public virtual IActionResult PatchMessagesMessageId([FromRoute][Required] string messageId)
        {
            return Ok($"ChangeMessage");
        }

        /// <summary>
        /// DeleteChatMessage
        /// </summary>
        /// <remarks>Delete chat message (for message senders, chat admins, admins).</remarks>        
        /// <param name="messageId"></param>        
        [HttpDelete]
        [Route("{messageId}")]
        public virtual IActionResult DeleteMessagesMessageId([FromRoute][Required] string messageId)
        {
            return Ok($"DeleteChatMessage");
        }

        /// <summary>
        /// GetChatMessageInfo
        /// </summary>
        /// <remarks>Get information about chat message.</remarks>        
        /// <param name="messageId"></param>
        [HttpGet]
        [Route("{messageId}")]
        public virtual IActionResult GetMessagesMessageId([FromRoute][Required] string messageId)
        {
            return Ok("GetChatMessageInfo");
        }
    }
}
