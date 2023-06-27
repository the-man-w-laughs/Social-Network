using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Messages.Request;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMessageService _messageService;
    private readonly IChatService _chatService;
    
    public MessagesController(IMapper mapper, IMessageService messageService, IChatService chatService)
    {
        _mapper = mapper;
        _messageService = messageService;
        _chatService = chatService;
    }
    //
    // /// <summary>
    // /// ReplyMessage
    // /// </summary>
    // /// <remarks>Reply chat message (for chat members).</remarks>
    // [HttpPost]
    // [Authorize(Roles = "User")]
    // [Route("{messageId}")]
    // public virtual async Task<ActionResult<MessageResponseDto>> PostMessagesMessageId(
    //     [FromRoute, Required] uint messageId,
    //     [FromBody, Required] MessageRequestDto messageRequestDto)
    // {
    //     var message = await _messageService.GetMessage(messageId);
    //
    //     if (message == null) return BadRequest($"Message with ID {messageId} doesn't exist");
    //
    //     var isUserAuthenticated =
    //         await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    //
    //     var userRole = isUserAuthenticated.Principal!.Claims
    //         .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;
    //
    //     var userId = uint.Parse(isUserAuthenticated.Principal!.Claims
    //         .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);
    //
    //     if (userRole == UserType.User.ToString())
    //     { 
    //         var isUserChatMember = await _chatService.IsUserChatMember(message.ChatId, userId);
    //         if (!isUserChatMember) return Forbid("User isn't chat member of chat");
    //     }
    //     
    //     var messageToAdd = new Message
    //     {
    //         Content = messageRequestDto.Content,
    //         CreatedAt = DateTime.Now,
    //         ChatId = message.ChatId,
    //         SenderId = userId,
    //         RepliedMessageId = messageId,
    //     };
    //
    //     var addedMessage = await _messageService.AddMessage(messageToAdd);
    //     
    //     return Ok(_mapper.Map<MessageResponseDto>(addedMessage));
    // }
    //
    // // TODO: aggregate message info
    // /// <summary>
    // /// GetMessageInfo    
    // /// </summary>
    // /// <remarks>Get information about chat message.</remarks>
    // [HttpGet]
    // [Authorize(Roles = "User")]
    // [Route("{messageId}")]
    // public virtual async Task<ActionResult<MessageResponseDto>> GetMessagesMessageId([FromRoute, Required] uint messageId)
    // {
    //     var message = await _messageService.GetMessage(messageId);
    //     return message == null
    //         ? BadRequest($"Message with ID {messageId} doesn't exist")
    //         : Ok(_mapper.Map<MessageResponseDto>(message));
    // }
    //
    // /// <summary>
    // /// ChangeMessage
    // /// </summary>
    // /// <remarks>Change chat message (for message senders).</remarks>
    // [HttpPatch]
    // [Route("{messageId}")]
    // public virtual ActionResult<MessageResponseDto> PatchMessagesMessageId(
    //     [FromRoute, Required] uint messageId,
    //     [FromBody, Required] MessagePatchRequestDto messagePatchRequestDto)
    // {
    //     var message = new Message { Id = 200, Content = "TestMessage", CreatedAt = DateTime.Now };
    //     
    //     return Ok(_mapper.Map<MessageResponseDto>(message));
    // }
    //
    // /// <summary>
    // /// DeleteChatMessage
    // /// </summary>
    // /// <remarks>Delete chat message (for message senders, chat admins, admins).</remarks>
    // [HttpDelete]
    // [Authorize(Roles = "User")]
    // [Route("{messageId}")]
    // public virtual async Task<ActionResult<MessageResponseDto>> DeleteMessagesMessageId([FromRoute, Required] uint messageId)
    // {
    //     var message = await _messageService.GetMessage(messageId);
    //
    //     if (message == null) return BadRequest($"Message with ID {messageId} doesn't exist");
    //
    //     var isUserAuthenticated =
    //         await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    //
    //     var userRole = isUserAuthenticated.Principal!.Claims
    //         .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;
    //
    //     var userId = uint.Parse(isUserAuthenticated.Principal!.Claims
    //         .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);
    //     
    //     if (userRole == UserType.User.ToString())
    //     {
    //         var isUserCanDeleteMessage = await _messageService.IsUserCanDeleteMessage(message, userId);
    //         if (!isUserCanDeleteMessage) return Forbid("You are can't delete message");
    //     }
    //
    //     await _messageService.DeleteMessage(message.Id);
    //     
    //     return Ok(_mapper.Map<MessageResponseDto>(message));
    // }
    //
    // /// <summary>
    // /// LikeMessage
    // /// </summary>
    // /// <remarks>Like chat message (for chat members).</remarks>
    // [HttpPost]
    // [Authorize(Roles = "User")]
    // [Route("{messageId}/likes")]
    // public virtual async Task<ActionResult<MessageLikeResponseDto>> PostMessagesMessageIdLikes([FromRoute, Required] uint messageId)
    // {
    //     var message = await _messageService.GetMessage(messageId);
    //     if (message == null) return BadRequest($"Message with ID {messageId} doesn't exist");
    //
    //     var isUserAuthenticated =
    //         await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    //
    //     var userRole = isUserAuthenticated.Principal!.Claims
    //         .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;
    //
    //     var userId = uint.Parse(isUserAuthenticated.Principal!.Claims
    //         .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);
    //     
    //     if (userRole == UserType.User.ToString())
    //     {
    //         var isUserChatMember = await _chatService.IsUserChatMember(message.ChatId, userId);
    //         if (!isUserChatMember) return Forbid("User isn't chat member of chat");
    //     }
    //
    //     var chatMember = (await _chatService.GetChatMember(message.ChatId, userId))!;
    //     var isUserAlreadyLikeMessage = await _messageService.IsChatMemberAlreadyLikeMessage(message.Id, chatMember.Id);
    //     if (isUserAlreadyLikeMessage) return Conflict("User already like this message");
    //     
    //     var messageLike = new MessageLike
    //     {
    //         MessageId = messageId,
    //         CreatedAt = DateTime.Now,
    //         ChatMemberId = chatMember.Id
    //     };
    //
    //     await _messageService.AddMessageLike(messageLike);
    //     
    //     return Ok(_mapper.Map<MessageLikeResponseDto>(messageLike));
    // }
    //
    // /// <summary>
    // /// GetAllMessageLikes
    // /// </summary>
    // /// <remarks>Get all message likes using pagination (for chat members).</remarks>
    // [HttpGet]
    // [Authorize(Roles = "User")]
    // [Route("{messageId}/likes")]
    // public virtual async Task<ActionResult<List<MessageLikeResponseDto>>> GetMessagesMessageId(
    //     [FromRoute, Required] uint messageId,
    //     [FromQuery, Required] int limit,
    //     [FromQuery, Required] int currCursor)
    // {
    //     var messageLikes = await _messageService.GetAllMessageLikesPaginated(messageId, limit, currCursor);
    //     
    //     return Ok(messageLikes.Select(ml => _mapper.Map<MessageLikeResponseDto>(ml)));
    // }
    //
    // /// <summary>
    // /// UnlikeMessage
    // /// </summary>
    // /// <remarks>Unlike chat message (for like owner).</remarks>
    // [HttpDelete]
    // [Authorize(Roles = "User")]
    // [Route("{messageId}/likes")]
    // public virtual async Task<ActionResult<MessageLikeResponseDto>> DeleteMessagesMessageIdLikes([FromRoute, Required] uint messageId)
    // {
    //     var message = await _messageService.GetMessage(messageId);
    //     if (message == null) return BadRequest($"Message with ID {messageId} doesn't exist");
    //
    //     var isUserAuthenticated =
    //         await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    //
    //     var userRole = isUserAuthenticated.Principal!.Claims
    //         .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;
    //
    //     var userId = uint.Parse(isUserAuthenticated.Principal!.Claims
    //         .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);
    //     
    //     if (userRole == UserType.User.ToString())
    //     {
    //         var isUserChatMember = await _chatService.IsUserChatMember(message.ChatId, userId);
    //         if (!isUserChatMember) return Forbid("User isn't chat member of chat");
    //     }
    //
    //     var chatMember = (await _chatService.GetChatMember(message.ChatId, userId))!;
    //     var isUserAlreadyLikeMessage = await _messageService.IsChatMemberAlreadyLikeMessage(message.Id, chatMember.Id);
    //     if (!isUserAlreadyLikeMessage) return Conflict("User didn't like this message");
    //
    //     var deletedLike = await _messageService.DeleteMessageLike(message.Id, chatMember.Id);
    //     
    //     return Ok(_mapper.Map<MessageLikeResponseDto>(deletedLike));
    // }
}