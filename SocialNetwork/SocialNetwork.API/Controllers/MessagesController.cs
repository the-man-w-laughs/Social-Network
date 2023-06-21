using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTO.Messages.Request;
using SocialNetwork.BLL.DTO.Messages.Response;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MessagesController : ControllerBase
{

    /// <summary>
    /// ReplyMessage
    /// </summary>
    /// <remarks>Reply chat message (for chat members).</remarks>          
    [HttpPost]
    [Route("{messageId}")]
    public virtual ActionResult<MessageResponseDto> PostMessagesMessageId([FromRoute][Required] uint messageId, [FromBody][Required] MessageRequestDto messageRequestDto)
    {
        return Ok(new MessageResponseDto());
    }

    // TODO: aggregate message info
    /// <summary>
    /// GetChatMessageInfo
    /// </summary>
    /// <remarks>Get information about chat message.</remarks>            
    [HttpGet]
    [Route("{messageId}")]
    public virtual IActionResult GetMessagesMessageId([FromRoute][Required] uint messageId)
    {
        return Ok("GetChatMessageInfo");
    }


    /// <summary>
    /// ChangeMessage
    /// </summary>
    /// <remarks>Change chat message (for message senders, chat admins, admins).</remarks>          
    [HttpPut]
    [Route("{messageId}")]
    public virtual ActionResult<MessageResponseDto> PutMessagesMessageId([FromRoute][Required] uint messageId, [FromBody][Required] MessageRequestDto messageRequestDto)
    {
        return Ok(new MessageResponseDto());
    }

    /// <summary>
    /// DeleteChatMessage
    /// </summary>
    /// <remarks>Delete chat message (for message senders, chat admins, admins).</remarks>                   
    [HttpDelete]
    [Route("{messageId}")]
    public virtual ActionResult<MessageResponseDto> DeleteMessagesMessageId([FromRoute][Required] uint messageId)
    {
        return Ok(new MessageResponseDto());
    }

    /// <summary>
    /// LikeMessage
    /// </summary>
    /// <remarks>Like chat message (for chat members).</remarks>        
    [HttpPost]
    [Route("{messageId}/likes")]
    public virtual ActionResult<MessageLikeResponseDto> PostMessagesMessageIdLikes([FromRoute][Required] uint messageId)
    {
        return Ok(new MessageLikeResponseDto());
    }

    /// <summary>
    /// GetAllMessageLikes
    /// </summary>
    /// <remarks>Get all message likes using pagination (for chat members).</remarks>    
    [HttpGet]
    [Route("{messageId}/likes")]
    public virtual ActionResult<List<MessageLikeResponseDto>> GetMessagesMessageId([FromRoute][Required] uint messageId, [FromQuery] uint? limit, [FromQuery] uint currCursor)
    {
        return Ok(new List<MessageLikeResponseDto>() { new MessageLikeResponseDto() });
    }

    /// <summary>
    /// UnlikeMessage
    /// </summary>
    /// <remarks>Unlike chat message (for like owner).</remarks>    
    [HttpDelete]
    [Route("{messageId}/likes")]
    public virtual ActionResult<MessageLikeResponseDto> DeleteMessagesMessageIdLikes([FromRoute][Required] uint messageId)
    {
        return Ok(new MessageLikeResponseDto());
    }
}