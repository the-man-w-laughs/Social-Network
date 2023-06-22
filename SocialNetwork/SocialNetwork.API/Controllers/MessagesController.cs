using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.DTO.Messages.Request;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMapper _mapper;

    public MessagesController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// ReplyMessage
    /// </summary>
    /// <remarks>Reply chat message (for chat members).</remarks>
    [HttpPost]
    [Route("{messageId}")]
    public virtual ActionResult<MessageResponseDto> PostMessagesMessageId(
        [FromRoute, Required] uint messageId,
        [FromBody, Required] MessageRequestDto messageRequestDto)
    {
        var message = new Message { Id = 200, Content = "TestMessage", CreatedAt = DateTime.Now };
        
        return Ok(_mapper.Map<MessageResponseDto>(message));
    }

    // TODO: aggregate message info
    /// <summary>
    /// GetChatMessageInfo
    /// </summary>
    /// <remarks>Get information about chat message.</remarks>
    [HttpGet]
    [Route("{messageId}")]
    public virtual ActionResult<MessageResponseDto> GetMessagesMessageId([FromRoute, Required] uint messageId)
    {
        var message = new Message { Id = 200, Content = "TestMessage", CreatedAt = DateTime.Now };
        
        return Ok(_mapper.Map<MessageResponseDto>(message));
    }
    
    /// <summary>
    /// ChangeMessage
    /// </summary>
    /// <remarks>Change chat message (for message senders, chat admins, admins).</remarks>
    [HttpPut]
    [Route("{messageId}")]
    public virtual ActionResult<MessageResponseDto> PutMessagesMessageId(
        [FromRoute, Required] uint messageId,
        [FromBody, Required] MessageRequestDto messageRequestDto)
    {
        var message = new Message { Id = 200, Content = "TestMessage", CreatedAt = DateTime.Now };
        
        return Ok(_mapper.Map<MessageResponseDto>(message));
    }

    /// <summary>
    /// DeleteChatMessage
    /// </summary>
    /// <remarks>Delete chat message (for message senders, chat admins, admins).</remarks>
    [HttpDelete]
    [Route("{messageId}")]
    public virtual ActionResult<MessageResponseDto> DeleteMessagesMessageId([FromRoute, Required] uint messageId)
    {
        var message = new Message { Id = 200, Content = "TestMessage", CreatedAt = DateTime.Now };
        
        return Ok(_mapper.Map<MessageResponseDto>(message));
    }

    /// <summary>
    /// LikeMessage
    /// </summary>
    /// <remarks>Like chat message (for chat members).</remarks>
    [HttpPost]
    [Route("{messageId}/likes")]
    public virtual ActionResult<MessageLikeResponseDto> PostMessagesMessageIdLikes([FromRoute, Required] uint messageId)
    {
        var messageLike = new MessageLike { Id = 200, CreatedAt = DateTime.Now };
        
        return Ok(_mapper.Map<MessageLikeResponseDto>(messageLike));
    }

    /// <summary>
    /// GetAllMessageLikes
    /// </summary>
    /// <remarks>Get all message likes using pagination (for chat members).</remarks>
    [HttpGet]
    [Route("{messageId}/likes")]
    public virtual ActionResult<List<MessageLikeResponseDto>> GetMessagesMessageId(
        [FromRoute, Required] uint messageId,
        [FromQuery, Required] uint? limit,
        [FromQuery, Required] uint currCursor)
    {
        var messageLikes = new List<MessageLike>
        {
            new() { Id = 200, CreatedAt = DateTime.Now },
            new() { Id = 201, CreatedAt = DateTime.Now.AddDays(-1) }
        };
        
        return Ok(messageLikes.Select(ml => _mapper.Map<MessageLikeResponseDto>(ml)));
    }

    /// <summary>
    /// UnlikeMessage
    /// </summary>
    /// <remarks>Unlike chat message (for like owner).</remarks>
    [HttpDelete]
    [Route("{messageId}/likes")]
    public virtual ActionResult<MessageLikeResponseDto> DeleteMessagesMessageIdLikes([FromRoute, Required] uint messageId)
    {
        var messageLike = new MessageLike { Id = 200, CreatedAt = DateTime.Now };
        
        return Ok(_mapper.Map<MessageLikeResponseDto>(messageLike));
    }
}