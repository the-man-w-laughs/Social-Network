using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTO.ChatDto.Request;
using SocialNetwork.BLL.DTO.ChatDto.Response;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Controllers
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
        public virtual ActionResult<DeleteChatDto> DeleteChatsChatId([FromRoute][Required]uint chatId)
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
        public virtual ActionResult<DeleteChatMemberDto> DeleteChatsChatIdMembersParticipantId([FromRoute][Required] uint chatId, [FromRoute][Required] uint memberId)
        {
            return Ok($"DeleteChatMember");
        }

        //TODO: Make Dto
        /// <summary>
        /// GetAllChatMedias
        /// </summary>
        /// <remarks>Get all chat medias (for chat members).</remarks>
        /// <param name="chatId"></param>
        /// <param name="limit"></param>
        /// <param name="nextCursor"></param>
        [HttpGet]
        [Route("{chatId}/medias")]
        public virtual IActionResult GetChatChatIdMedias([FromRoute][Required] uint chatId, [FromQuery] uint? limit, [FromQuery] uint? nextCursor)
        {
            return Ok($"GetAllChatMedias");
        }

        // TODO: MAKE DTO
        /// <summary>
        /// GetChatInfo
        /// </summary>
        /// <remarks>Get chat information (name, photo). For chat members.</remarks>
        /// <param name="chatId"></param>        
        [HttpGet]
        [Route("chats/{chatId}")]
        public virtual IActionResult GetChatsChatId([FromRoute][Required] uint chatId)
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
        public virtual ActionResult<List<DeleteChatMemberDto>> GetChatsChatIdMembers([FromRoute][Required] uint chatId, [FromQuery][Required()] uint? limit, [FromQuery] uint? nextCursor)
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
        public virtual ActionResult<List<GetAllChatMessagesDto>> GetChatsChatIdMessages([FromRoute][Required] uint chatId, [FromQuery][Required()] uint? limit_, [FromQuery] uint? nextCursor)
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
        public virtual ActionResult<DeleteChatDto> PatchChatsChatId([FromRoute][Required] uint chatId, [FromBody][Required] PostChatDto deleteChatDto)
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
        public virtual ActionResult<DeleteChatMemberDto> PatchChatsChatIdMembersMemberId([FromRoute][Required]uint chatId, [FromRoute][Required] uint memberId,[FromBody][Required] ChangeChatMemberStatusDto changeChatMemberStatusDto)
        {
            return Ok("ChangeMemberStatus");
        }

        /// <summary>
        /// CreateChat
        /// </summary>
        /// <remarks>Create new chat</remarks>        
        [HttpPost]        
        public virtual IActionResult PostChats([FromBody][Required] PostChatDto deleteChatDto)
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
        public virtual ActionResult<PostChatMemberResponseDto> PostChatsChatIdMembers([FromRoute][Required]uint chatId, [FromBody][Required] PostChatMemberRequestDto postChatMemberDto)
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
        public virtual ActionResult<PostMessageResponseDto> PostChatsChatIdMessages([FromRoute][Required]string chatId, [FromBody][Required] PostMessageRequestDto postChatMemberDto)
        {
            return Ok("SendMessage");
        }
    }
}
