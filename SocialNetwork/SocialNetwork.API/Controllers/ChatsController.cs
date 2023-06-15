using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace IO.Swagger.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    { 
        /// <summary>
        /// DeleteChat
        /// </summary>
        /// <remarks>Delete chat. For chat admins.</remarks>
        /// <param name="chatId"></param>        
        [HttpDelete]
        [Route("{chatId}")]
        public virtual IActionResult DeleteChatsChatId([FromRoute][Required]uint chatId)
        {             
            return Ok($"DeleteChat");            
        }

        /// <summary>
        /// DeleteChatMember
        /// </summary>
        /// <remarks>Delete a member from chat (for chat admins, admins).</remarks>
        /// <param name="chatId"></param>
        /// <param name="memberId"></param>        
        [HttpDelete]
        [Route("{chatId}/members/{memberId}")]
        public virtual IActionResult DeleteChatsChatIdMembersParticipantId([FromRoute][Required]string chatId, [FromRoute][Required]string memberId)
        {
            return Ok($"DeleteChatMember");
        }

        /// <summary>
        /// GetAllChatMedias
        /// </summary>
        /// <remarks>Get all chat medias (for chat members).</remarks>
        /// <param name="chatId"></param>
        /// <param name="limit"></param>
        /// <param name="nextCursor"></param>
        [HttpGet]
        [Route("{chatId}/medias")]
        public virtual IActionResult GetChatChatIdMedias([FromRoute][Required]string chatId, [FromQuery]decimal? limit, [FromQuery]decimal? nextCursor)
        {
            return Ok($"GetAllChatMedias");
        }

        /// <summary>
        /// GetChatInfo
        /// </summary>
        /// <remarks>Get chat information (name, photo). For chat members.</remarks>
        /// <param name="chatId"></param>        
        [HttpGet]
        [Route("chats/{chatId}")]
        public virtual IActionResult GetChatsChatId([FromRoute][Required]string chatId)
        {
            return Ok($"GetChatInfo");
        }

        /// <summary>
        /// GetAllChatMembers
        /// </summary>
        /// <remarks>Get all chat members using pagination (for chat members).</remarks>
        /// <param name="chatId"></param>
        /// <param name="limit"></param>
        /// <param name="nextCursor"></param>
        [HttpGet]
        [Route("{chatId}/members")]
        public virtual IActionResult GetChatsChatIdMembers([FromRoute][Required]string chatId, [FromQuery][Required()]decimal? limit, [FromQuery]decimal? nextCursor)
        {
            return Ok("GetAllChatMembers");
        }

        /// <summary>
        /// GetAllChatMessages
        /// </summary>
        /// <remarks>Get all chat messages using pagination (for chat members). </remarks>
        /// <param name="chatId"></param>
        /// <param name="limit_"></param>
        /// <param name="nextCursor"></param>
        [HttpGet]
        [Route("{chatId}/messages")]
        public virtual IActionResult GetChatsChatIdMessages([FromRoute][Required]string chatId, [FromQuery][Required()]decimal? limit_, [FromQuery]decimal? nextCursor)
        {
            return Ok("GetAllChatMessages");
        }

        /// <summary>
        /// ChangeChatInfo
        /// </summary>
        /// <remarks>Change chat information (name, photo). For chat admins.</remarks>
        /// <param name="chatId"></param>        
        [HttpPatch]
        [Route("{chatId}")]        
        public virtual IActionResult PatchChatsChatId([FromRoute][Required]string chatId)
        {
            return Ok("ChangeChatInfo");
        }

        /// <summary>
        /// ChangeMemberStatus
        /// </summary>
        /// <remarks>Updates information about chat member (for chat admins, admins).</remarks>
        /// <param name="chatId"></param>
        /// <param name="memberId"></param>        
        [HttpPatch]
        [Route("{chatId}/members/{memberId}")]
        public virtual IActionResult PatchChatsChatIdMembersMemberId([FromRoute][Required]string chatId, [FromRoute][Required]string memberId)
        {
            return Ok("ChangeMemberStatus");
        }

        /// <summary>
        /// CreateChat
        /// </summary>
        /// <remarks>Create new chat</remarks>        
        [HttpPost]        
        public virtual IActionResult PostChats()
        {
            return Ok("CreateChat");
        }

        /// <summary>
        /// AddChatMember
        /// </summary>
        /// <remarks>Add new member to chat (for chat members).</remarks>
        /// <param name="chatId"></param>        
        [HttpPost]
        [Route("{chatId}/members")]
        public virtual IActionResult PostChatsChatIdMembers([FromRoute][Required]string chatId)
        {
            return Ok("AddChatMember");
        }

        /// <summary>
        /// SendMessage
        /// </summary>
        /// <remarks>Send a message to chat.</remarks>
        /// <param name="chatId"></param>        
        [HttpPost]
        [Route("{chatId}/messages")]
        public virtual IActionResult PostChatsChatIdMessages([FromRoute][Required]string chatId)
        {
            return Ok("SendMessage");
        }
    }
}
