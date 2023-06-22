using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTO.Messages.Request;
using SocialNetwork.BLL.DTO.Messages.Response;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MediasController : ControllerBase
{

    /// <summary>
    /// GetAllMediasObjects
    /// </summary>
    /// <remarks>Reply chat message (for chat members).</remarks>          
    [HttpGet]    
    public virtual ActionResult<MessageResponseDto> GetMedias([FromQuery] uint? limit, [FromQuery] uint currCursor)
    {
        return Ok();
    }

    /// <summary>
    /// GetMedia
    /// </summary>
    /// <remarks>Reply chat message (for chat members).</remarks>          
    [HttpGet]
    [Route("{mediaId}")]
    public virtual ActionResult<MessageResponseDto> GetMediasMediaId([FromRoute][Required] uint messageId)
    {
        return Ok();
    }

    /// <summary>
    /// DeleteMedia
    /// </summary>
    /// <remarks>Delete chat message (for message senders, chat admins, admins).</remarks>                   
    [HttpDelete]
    [Route("{messageId}")]
    public virtual ActionResult<MessageResponseDto> DeleteMessagesMessageId([FromRoute][Required] uint messageId)
    {
        return Ok(new MessageResponseDto());
    }

    /// <summary>
    /// LikeMedia
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
    [Route("{mediaId}/likes")]
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