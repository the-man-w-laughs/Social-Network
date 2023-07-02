using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Middlewares;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Messages.Request;
using SocialNetwork.BLL.DTO.Messages.Response;

namespace SocialNetwork.API.Controllers;

[Authorize(Roles = "User")]
[ApiController, Route("[controller]")]
public class ChatsController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatsController(IChatService chatService)
    {
        _chatService = chatService;
    }

    /// <summary>Create Chat</summary>
    /// <remarks>Create a new chat.</remarks>    
    /// <param name="chatRequestDto">The chat request data transfer object.</param>    
    /// <response code="200">Returns a <see cref="ChatResponseDto"/> with the details of the newly created chat.</response>
    /// <response code="401">Returns a string message if the user unauthorized.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ChatResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public virtual async Task<ActionResult<ChatResponseDto>> PostChats(
        [FromBody, Required] ChatPostDto chatRequestDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var addedChat = await _chatService.CreateChat(userId, chatRequestDto);
        return Ok(addedChat);
    }

    /// <summary>Get Chat</summary>
    /// <remarks>Get chat information (name, photo) by chat ID (for chat members).</remarks>
    /// <param name="chatId">The ID of the chat to retrieve information for.</param>
    /// <response code="200">Returns a <see cref="ChatResponseDto"/> with details of the chat.</response>
    /// <response code="400">Returns a string message if the chat doesn't exist.</response>
    /// <response code="401">Returns a string message if the user unauthorized.</response>
    /// <response code="403">Returns a string message if the user not a chat member.</response>
    [HttpGet, Route("{chatId}")]
    [ProducesResponseType(typeof(ChatResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<ChatResponseDto>> GetChatsChatId([FromRoute, Required] uint chatId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var chatInfo = await _chatService.GetChatInfo(chatId, userId);
        return Ok(chatInfo);
    }

    /// <summary>Change Chat</summary>
    /// <remarks>Change chat information (name, photo) by chat ID (for chat admins, owners).</remarks>
    /// <param name="chatId">The ID of the chat to change information for.</param>
    /// <param name="chatPatchRequestDto">The chat patch request data transfer object.</param>    
    /// <response code="200">Returns a <see cref="ChatResponseDto"/> with details of the updated chat information.</response>
    /// <response code="400">Returns a string message if the chat doesn't exist or an error occurred during the update.</response>
    /// <response code="401">Returns a string message if the user unauthorized.</response>
    /// <response code="403">Returns a string message if the user not a chat owner.</response>
    [HttpPatch, Route("{chatId}")]
    [ProducesResponseType(typeof(ChatResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<ChatResponseDto>> PatchChatsChatId(
        [FromRoute, Required] uint chatId,
        [FromBody, Required] ChatPatchDto chatPatchRequestDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var updatedChat = await _chatService.UpdateChat(chatId, userId, chatPatchRequestDto);
        return Ok(updatedChat);
    }

    /// <summary>Delete Chat</summary>
    /// <remarks>Delete chat by ID (for owners).</remarks>
    /// <param name="chatId">The ID of the chat to delete.</param>
    /// <response code="200">Returns a <see cref="ChatResponseDto"/> with details of the deleted chat.</response>
    /// <response code="400">Returns a string message if the chat doesn't exist.</response>
    /// <response code="401">Returns a string message if the user is unauthorized.</response>
    /// <response code="403">Returns a string message if the user not a chat owner.</response>
    [HttpDelete, Route("{chatId}")]
    [ProducesResponseType(typeof(ChatResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<ChatResponseDto>> DeleteChatsChatId([FromRoute, Required] uint chatId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var deletedChat = await _chatService.DeleteChat(chatId, userId);
        return Ok(deletedChat);
    }

    /// <summary>Add Chat Member</summary>
    /// <remarks>Adds a new member to the chat (for chat members).</remarks>
    /// <param name="chatId">The ID of the chat to add a member to.</param>
    /// <param name="userToAddId">The id of user to add.</param>    
    /// <response code="200">Returns a <see cref="ChatMemberResponseDto"/> the added chat member information.</response>
    /// <response code="400">Returns a string message if the chat doesn't exist.</response>
    /// <response code="401">Returns a string message if the user unauthorized.</response>
    /// <response code="403">Returns a string message if the user not a chat member.</response>
    /// <response code="409">Returns a string message if the user is already a member of the chat.</response>
    [HttpPost, Route("{chatId}/members")]
    [ProducesResponseType(typeof(ChatMemberResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public virtual async Task<ActionResult<ChatMemberResponseDto>> PostChatsChatIdMembers(
        [FromRoute, Required] uint chatId,
        [FromBody, Required] uint userToAddId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var addedChatMember = await _chatService.AddChatMember(userId, chatId, userToAddId);
        return Ok(addedChatMember);
    }

    /// <summary>Get Chat Members</summary>
    /// <remarks>Get all chat members by chat ID using pagination (for chat members).</remarks>
    /// <param name="chatId">The ID of the chat.</param>
    /// <param name="limit">The maximum number of chat members to retrieve.</param>
    /// <param name="nextCursor">The cursor for pagination to retrieve the next set of chat members.</param>    
    /// <response code="200">Returns a list of <see cref="ChatMemberResponseDto"/> with details of each chat member.</response>
    /// <response code="400">Returns a string message if the chat doesn't exist.</response>
    /// <response code="401">Returns a string message if the user unauthorized.</response>
    /// <response code="403">Returns a string message if the user not a chat member.</response>
    [HttpGet, Route("{chatId}/members")]
    [ProducesResponseType(typeof(List<ChatMemberResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<List<ChatMemberResponseDto>>> GetChatsChatIdMembers(
        [FromRoute, Required] uint chatId,
        [FromQuery, Required] int limit,
        [FromQuery] int nextCursor)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var chatMembers = await _chatService.GetChatMembers(userId, chatId, limit, nextCursor);
        return Ok(chatMembers);
    }

    /// <summary>Change Member Status</summary>
    /// <remarks>Updates the information about a chat member (for chat admins, admins).</remarks>
    /// <param name="chatId">The ID of the chat.</param>
    /// <param name="memberId">The ID of the chat member.</param>
    /// <param name="changeChatMemberRequestDto">The change chat member request data transfer object.</param>    
    /// <response code="200">Returns a <see cref="ChatMemberResponseDto"/> with details of the updated chat member status.</response>
    /// <response code="400">Returns a string message if the chat or chat member doesn't exist.</response>
    /// <response code="401">Returns a string message if the user unauthorized.</response>
    /// <response code="403">Returns a string message if the user doesn't have permission.</response>
    [HttpPut, Route("{chatId}/members/{memberId}")]
    [ProducesResponseType(typeof(ChatMemberResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<ChatMemberResponseDto>> PatchChatsChatIdMembersMemberId(
        [FromRoute, Required] uint chatId,
        [FromRoute, Required] uint memberId,
        [FromBody, Required] ChatMemberPutDto changeChatMemberRequestDto)
    {        
        var userId = HttpContext.GetAuthenticatedUserId();
        var updatedChatMember = await _chatService.UpdateChatMember(chatId, userId, memberId, changeChatMemberRequestDto);
        return Ok(updatedChatMember);
    }

    /// <summary>Delete Chat Member</summary>
    /// <remarks>Delete a member from the chat (for chat admins, owners).</remarks>
    /// <param name="chatId">The ID of the chat.</param>
    /// <param name="userToDeleteId">The ID of the user to delete from the chat.</param>    
    /// <response code="200">Returns the deleted chat member information.</response>
    /// <response code="400">Returns a string message if the chat doesn't exist.</response>
    /// <response code="401">Returns a string message if the user unauthorized.</response>
    /// <response code="403">Returns a string message if the user doesn't have permission.</response>
    /// <response code="404">Returns a string message if the chat member is not found.</response>
    [HttpDelete, Route("{chatId}/members/{userToDeleteId}")]
    [ProducesResponseType(typeof(ChatMemberResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult<ChatMemberResponseDto>> DeleteChatsChatIdMembersParticipantId(
        [FromRoute, Required] uint chatId,
        [FromRoute, Required] uint userToDeleteId)
    {        
        var userId = HttpContext.GetAuthenticatedUserId();
        var deletedMember = await _chatService.DeleteChatMember(userId, userToDeleteId, chatId);
        return Ok(deletedMember);
    }

    /// <summary>Get Chat Medias</summary>
    /// <remarks>Get all chat medias by chat ID (for chat members).</remarks>
    /// <param name="chatId">The ID of the chat to retrieve medias for.</param>
    /// <param name="limit">The maximum number of medias to retrieve.</param>
    /// <param name="nextCursor">The cursor value for pagination.</param>    
    /// <response code="200">Returns a list of <see cref="MediaResponseDto"/> with details of each chat media.</response>
    /// <response code="400">Returns a string message if the chat doesn't exist.</response>
    /// <response code="401">Returns a string message if the user unauthorized.</response>
    /// <response code="403">Returns a string message if the user not a chat member.</response>
    [HttpGet, Route("{chatId}/medias")]
    [ProducesResponseType(typeof(List<MediaResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<List<MediaResponseDto>>> GetChatChatIdMedias(
        [FromRoute, Required] uint chatId,
        [FromQuery, Required] int limit,
        [FromQuery] int nextCursor)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var chatMedias = await _chatService.GetChatMedias(userId, chatId, limit, nextCursor);
        return Ok(chatMedias);
    }

    /// <summary>Get Chat Messages</summary>
    /// <remarks>Get all chat messages using pagination (for chat members).</remarks>
    /// <param name="chatId">The ID of the chat.</param>
    /// <param name="limit">The maximum number of messages to retrieve.</param>
    /// <param name="nextCursor">The cursor value for pagination.</param>    
    /// <response code="200">Returns a list of chat messages.</response>
    /// <response code="400">Returns a string message if the chat does not exist.</response>
    /// <response code="401">Returns a string message if the user unauthorized.</response>
    /// <response code="403">Returns a string message if the user not a chat member.</response>
    [HttpGet, Route("{chatId}/messages")]
    [ProducesResponseType(typeof(List<MessageResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<List<MessageResponseDto>>> GetChatsChatIdMessages(
        [FromRoute, Required] uint chatId,
        [FromQuery, Required] int limit,
        [FromQuery] int nextCursor)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var chatMessages = await _chatService.GetChatMessages(chatId, userId, limit, nextCursor);
        return Ok(chatMessages);
    }
}