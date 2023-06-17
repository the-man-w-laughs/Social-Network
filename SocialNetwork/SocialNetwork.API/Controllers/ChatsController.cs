using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTO.ChatDto.Request;
using SocialNetwork.BLL.DTO.ChatDto.Response;
using SocialNetwork.BLL.DTO.Messages.Request;
using SocialNetwork.BLL.DTO.Messages.Response;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatsController : ControllerBase
{ 
    /// <summary>
    /// DeleteChat
    /// </summary>
    /// <remarks>Delete chat. For chat admins.</remarks>      
    [HttpDelete]
    [Route("{chatId}")]
    public virtual ActionResult<ChatResponseDto> DeleteChatsChatId([FromRoute][Required]uint chatId)
    {
        return new ChatResponseDto { Id = chatId, CreatedAt = DateTime.Now, Name = "Svin" };
    }

    /// <summary>
    /// DeleteChatMember
    /// </summary>
    /// <remarks>Delete a member from chat (for chat admins, admins).</remarks>         
    [HttpDelete]
    [Route("{chatId}/members/{memberId}")]
    public virtual ActionResult<ChatMemberResponseDto> DeleteChatsChatIdMembersParticipantId([FromRoute][Required] uint chatId, [FromRoute][Required] uint memberId)
    {
        return new ChatMemberResponseDto();
    }

    // TODO: make DTO
    /// <summary>
    /// GetAllChatMedias
    /// </summary>
    /// <remarks>Get all chat medias (for chat members).</remarks>    
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
    [HttpGet]
    [Route("{chatId}")]
    public virtual IActionResult GetChatsChatId([FromRoute][Required] uint chatId)
    {
        return Ok($"GetChatInfo");
    }

    /// <summary>
    /// GetAllChatMembers
    /// </summary>
    /// <remarks>Get all chat members using pagination (for chat members).</remarks>    
    [HttpGet]
    [Route("{chatId}/members")]
    public virtual ActionResult<List<ChatMemberResponseDto>> GetChatsChatIdMembers([FromRoute][Required] uint chatId, [FromQuery][Required()] uint? limit, [FromQuery] uint? nextCursor)
    {
        return new List<ChatMemberResponseDto>() { new ChatMemberResponseDto()};
    }

    /// <summary>
    /// GetAllChatMessages
    /// </summary>
    /// <remarks>Get all chat messages using pagination (for chat members). </remarks>    
    [HttpGet]
    [Route("{chatId}/messages")]
    public virtual ActionResult<List<MessageResponseDto>> GetChatsChatIdMessages([FromRoute][Required] uint chatId, [FromQuery][Required()] uint? limit_, [FromQuery] uint? nextCursor)
    {
        return new List<MessageResponseDto>() { new MessageResponseDto()};
    }

    /// <summary>
    /// ChangeChatInfo
    /// </summary>
    /// <remarks>Change chat information (name, photo). For chat admins.</remarks>        
    [HttpPatch]
    [Route("{chatId}")]        
    public virtual ActionResult<ChatResponseDto> PatchChatsChatId([FromRoute][Required] uint chatId, [FromBody][Required] ChatRequestDto deleteChatDto)
    {
        return new ChatResponseDto();
    }

    /// <summary>
    /// ChangeMemberStatus
    /// </summary>
    /// <remarks>Updates information about chat member (for chat admins, admins).</remarks>    
    [HttpPatch]
    [Route("{chatId}/members/{memberId}")]
    public virtual ActionResult<ChatMemberResponseDto> PatchChatsChatIdMembersMemberId([FromRoute][Required]uint chatId, [FromRoute][Required] uint memberId,[FromBody][Required] ChatMemberRequestDto changeChatMemberStatusDto)
    {
        return new ChatMemberResponseDto();
    }

    /// <summary>
    /// CreateChat
    /// </summary>
    /// <remarks>Create new chat</remarks>        
    [HttpPost]        
    public virtual ActionResult<ChatResponseDto> PostChats([FromBody][Required] ChatRequestDto deleteChatDto)
    {
        return new ChatResponseDto();
    }

    /// <summary>
    /// AddChatMember
    /// </summary>
    /// <remarks>Add new member to chat (for chat members).</remarks>        
    [HttpPost]
    [Route("{chatId}/members")]
    public virtual ActionResult<ChatMemberResponseDto> PostChatsChatIdMembers([FromRoute][Required]uint chatId, [FromBody][Required] ChatMemberRequestDto postChatMemberDto)
    {
        return new ChatMemberResponseDto();
    }

    /// <summary>
    /// SendMessage
    /// </summary>
    /// <remarks>Send a message to chat.</remarks>     
    [HttpPost]
    [Route("{chatId}/messages")]
    public virtual ActionResult<MessageResponseDto> PostChatsChatIdMessages([FromRoute][Required] uint chatId, [FromBody][Required] MessageRequestDto postChatMemberDto)
    {
        return new MessageResponseDto();
    }
}