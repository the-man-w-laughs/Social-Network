using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTO.ChatDto.Request;
using SocialNetwork.BLL.DTO.ChatDto.Response;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
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
        return new DeleteChatDto { Id = chatId, CreatedAt = DateTime.Now, Name = "Svin" };
    }

    /// <summary>
    /// DeleteChatMember
    /// </summary>
    /// <remarks>Delete a member from chat (for chat admins, admins).</remarks>
    /// <param name="chatId"></param>
    /// <param name="memberId"></param>        
    [HttpDelete]
    [Route("{chatId}/members/{memberId}")]
    public virtual ActionResult<PostChatMemberResponseDto> DeleteChatsChatIdMembersParticipantId([FromRoute][Required] uint chatId, [FromRoute][Required] uint memberId)
    {
        return new PostChatMemberResponseDto();
    }

    // TODO: make DTO
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

    // TODO: make DTO
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
    public virtual ActionResult<List<PostChatMemberResponseDto>> GetChatsChatIdMembers([FromRoute][Required] uint chatId, [FromQuery][Required()] uint? limit, [FromQuery] uint? nextCursor)
    {
        return new List<PostChatMemberResponseDto>();
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
        return new List<GetAllChatMessagesDto>();
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
        return new DeleteChatDto();
    }

    /// <summary>
    /// ChangeMemberStatus
    /// </summary>
    /// <remarks>Updates information about chat member (for chat admins, admins).</remarks>
    /// <param name="chatId"></param>
    /// <param name="memberId"></param>        
    [HttpPatch]
    [Route("{chatId}/members/{memberId}")]
    public virtual ActionResult<PostChatMemberResponseDto> PatchChatsChatIdMembersMemberId([FromRoute][Required]uint chatId, [FromRoute][Required] uint memberId,[FromBody][Required] PostChatMemberRequestDto changeChatMemberStatusDto)
    {
        return new PostChatMemberResponseDto();
    }

    /// <summary>
    /// CreateChat
    /// </summary>
    /// <remarks>Create new chat</remarks>        
    [HttpPost]        
    public virtual ActionResult<DeleteChatDto> PostChats([FromBody][Required] PostChatDto deleteChatDto)
    {
        return new DeleteChatDto();
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
        return new PostChatMemberResponseDto();
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
        return new PostMessageResponseDto();
    }
}