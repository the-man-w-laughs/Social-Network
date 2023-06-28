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

[ApiController]
[Route("[controller]")]
public class ChatsController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatsController(IChatService chatService)
    {
        _chatService = chatService;
    }

    /// <summary>
    /// CreateChat.
    /// </summary>
    /// <remarks>Create a new chat.</remarks>    
    /// <param name="chatRequestDto">The chat request data transfer object.</param>    
    /// <response code="200">Returns a <see cref="ChatResponseDto"/> with the details of the newly created chat.</response>
    /// <response code="401">Returns a string message if the user is unauthorized.</response>
    [HttpPost]
    [Authorize(Roles = "User")]
    [ProducesResponseType(typeof(ChatResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public virtual async Task<ActionResult<ChatResponseDto>> PostChats(
        [FromBody, Required] ChatRequestDto chatRequestDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var addedChat = await _chatService.CreateChat(chatRequestDto, userId);
        return Ok(addedChat);
    }

    /// <summary>
    /// GetChatInfo.
    /// </summary>
    /// <remarks>Get chat information (name, photo) for chat members.</remarks>
    /// <param name="chatId">The ID of the chat to retrieve information for.</param>    
    /// <response code="200">Returns the chat information.</response>
    /// <response code="400">Returns a string message if the chat does not exist.</response>
    /// <response code="401">Returns a string message if the user is unauthorized or not a chat member.</response>
    [HttpGet]
    [Authorize(Roles = "User")]
    [Route("{chatId}")]
    [ProducesResponseType(typeof(ChatResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public virtual async Task<ActionResult<ChatResponseDto>> GetChatsChatId([FromRoute, Required] uint chatId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var chatInfo = await _chatService.GetChatInfo(chatId, userId);
        return Ok(chatInfo);
    }

    /// <summary>
    /// GetAllChatMedias.
    /// </summary>
    /// <remarks>Get all chat medias for chat members.</remarks>
    /// <param name="chatId">The ID of the chat to retrieve medias for.</param>
    /// <param name="limit">The maximum number of medias to retrieve.</param>
    /// <param name="nextCursor">The cursor value for pagination.</param>    
    /// <response code="200">Returns the list of chat medias.</response>
    /// <response code="400">Returns a string message if the chat does not exist.</response>
    /// <response code="401">Returns a string message if the user is unauthorized or not a chat member.</response>
    [HttpGet]
    [Authorize(Roles = "User")]
    [Route("{chatId}/medias")]
    [ProducesResponseType(typeof(List<MediaResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public virtual async Task<ActionResult<List<MediaResponseDto>>> GetChatChatIdMedias(
        [FromRoute, Required] uint chatId,
        [FromQuery, Required] int limit,
        [FromQuery] int nextCursor)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var chatMedias = await _chatService.GetChatMedias(userId, chatId, limit, nextCursor);
        return Ok(chatMedias);
    }

    /// <summary>
    /// ChangeChatInfo.
    /// </summary>
    /// <remarks>Change chat information (name, photo) for chat admins.</remarks>
    /// <param name="chatId">The ID of the chat to change information for.</param>
    /// <param name="chatPatchRequestDto">The chat patch request data transfer object.</param>    
    /// <response code="200">Returns the updated chat information.</response>
    /// <response code="400">Returns a string message if the chat does not exist or an error occurred during the update.</response>
    /// <response code="401">Returns a string message if the user is unauthorized or not a chat owner.</response>
    [HttpPatch]
    [Authorize(Roles = "User")]
    [Route("{chatId}")]
    [ProducesResponseType(typeof(ChatResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public virtual async Task<ActionResult<ChatResponseDto>> PatchChatsChatId(
        [FromRoute, Required] uint chatId,
        [FromBody, Required] ChatPatchRequestDto chatPatchRequestDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var updatedChat = await _chatService.UpdateChat(chatId, userId, chatPatchRequestDto);
        return Ok(updatedChat);
    }

    /// <summary>
    /// DeleteChat.
    /// </summary>
    /// <remarks>Delete chat for chat admins.</remarks>
    /// <param name="chatId">The ID of the chat to delete.</param>    
    /// <response code="200">Returns the deleted chat information.</response>
    /// <response code="400">Returns a string message if the chat does not exist.</response>
    /// <response code="403">Returns a string message if the user is unauthorized or not a chat owner.</response>
    [HttpDelete]
    [Authorize(Roles = "User")]
    [Route("{chatId}")]
    [ProducesResponseType(typeof(ChatResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<ChatResponseDto>> DeleteChatsChatId([FromRoute, Required] uint chatId)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var deletedChat = await _chatService.DeleteChat(chatId, userId);
        return Ok(deletedChat);
    }

    /// <summary>
    /// AddChatMember.
    /// </summary>
    /// <remarks>Adds a new member to the chat (for chat members).</remarks>
    /// <param name="chatId">The ID of the chat to add a member to.</param>
    /// <param name="postChatMemberDto">The chat member request data transfer object.</param>    
    /// <response code="200">Returns the added chat member information.</response>
    /// <response code="400">Returns a string message if the chat does not exist.</response>
    /// <response code="403">Returns a string message if the user is unauthorized or not a chat member.</response>
    /// <response code="409">Returns a string message if the user is already a member of the chat.</response>
    [HttpPost]
    [Authorize(Roles = "User")]
    [Route("{chatId}/members")]
    [ProducesResponseType(typeof(ChatMemberResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public virtual async Task<ActionResult<ChatMemberResponseDto>> PostChatsChatIdMembers(
        [FromRoute, Required] uint chatId,
        [FromBody, Required] ChatMemberRequestDto postChatMemberDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var addedChatMember = await _chatService.AddChatMember(userId, chatId, postChatMemberDto);
        return Ok(addedChatMember);
    }

    /// <summary>
    /// GetAllChatMembers.
    /// </summary>
    /// <remarks>Get all chat members using pagination (for chat members).</remarks>
    /// <param name="chatId">The ID of the chat.</param>
    /// <param name="limit">The maximum number of chat members to retrieve.</param>
    /// <param name="nextCursor">The cursor for pagination to retrieve the next set of chat members.</param>    
    /// <response code="200">Returns a list of chat members.</response>
    /// <response code="400">Returns a string message if the chat does not exist.</response>
    /// <response code="403">Returns a string message if the user is unauthorized or not a chat member.</response>
    [HttpGet]
    [Authorize(Roles = "User")]
    [Route("{chatId}/members")]
    [ProducesResponseType(typeof(List<ChatMemberResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// ChangeMemberStatus.
    /// </summary>
    /// <remarks>Updates the information about a chat member (for chat admins, admins).</remarks>
    /// <param name="chatId">The ID of the chat.</param>
    /// <param name="memberId">The ID of the chat member.</param>
    /// <param name="changeChatMemberRequestDto">The change chat member request data transfer object.</param>    
    /// <response code="200">Returns a string message indicating the updated chat member status.</response>
    /// <response code="400">Returns a string message if the chat or chat member does not exist.</response>
    /// <response code="403">Returns a string message if the user is unauthorized or does not have permission.</response>
    [HttpPut]
    [Authorize(Roles = "User")]
    [Route("{chatId}/members/{memberId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<ChangeChatMemberResponseDto>> PatchChatsChatIdMembersMemberId(
        [FromRoute, Required] uint chatId,
        [FromRoute, Required] uint memberId,
        [FromBody, Required] ChangeChatMemberRequestDto changeChatMemberRequestDto)
    {
        //TODO РАЗОБРАТЬСЯ С ЛОГИКОЙ!!!!
        var userId = HttpContext.GetAuthenticatedUserId();
        var updatedChatMember = await _chatService
            .UpdateChatMember(chatId, userId, memberId, changeChatMemberRequestDto);
        return Ok(updatedChatMember);
    }

    /// <summary>
    /// DeleteChatMember.
    /// </summary>
    /// <remarks>Delete a member from the chat (for chat admins, admins).</remarks>
    /// <param name="chatId">The ID of the chat.</param>
    /// <param name="userToDeleteId">The ID of the user to delete from the chat.</param>    
    /// <response code="200">Returns the deleted chat member information.</response>
    /// <response code="400">Returns a string message if the chat does not exist.</response>
    /// <response code="403">Returns a string message if the user is unauthorized or does not have permission.</response>
    /// <response code="404">Returns a string message if the chat member is not found.</response>
    [HttpDelete]
    [Authorize(Roles = "User")]
    [Route("{chatId}/members/{userToDeleteId}")]
    [ProducesResponseType(typeof(ChatMemberResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult<ChatMemberResponseDto>> DeleteChatsChatIdMembersParticipantId(
        [FromRoute, Required] uint chatId,
        [FromRoute, Required] uint userToDeleteId)
    {
        //TODO РАЗОБРАТЬСЯ С ЛОГИКОЙ!!!!
        var userId = HttpContext.GetAuthenticatedUserId();
        var deletedMember = await _chatService.DeleteChatMember(userId, userToDeleteId, chatId);
        return Ok(deletedMember);
    }

    /// <summary>
    /// SendMessage.
    /// </summary>
    /// <remarks>Send a message to the chat.</remarks>
    /// <param name="chatId">The ID of the chat.</param>
    /// <param name="postChatMemberDto">The message request data transfer object.</param>    
    /// <response code="200">Returns the sent message information.</response>
    /// <response code="400">Returns a string message if the chat does not exist.</response>
    /// <response code="403">Returns a string message if the user is unauthorized or is not a chat member.</response>
    [HttpPost]
    [Authorize(Roles = "User")]
    [Route("{chatId}/messages")]
    [ProducesResponseType(typeof(MessageResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<MessageResponseDto>> PostChatsChatIdMessages(
        [FromRoute, Required] uint chatId,
        [FromBody, Required] MessageRequestDto postChatMemberDto)
    {
        var userId = HttpContext.GetAuthenticatedUserId();
        var addedMessage = await _chatService.SendMessage(chatId, userId, postChatMemberDto);
        return Ok(addedMessage);
    }

    /// <summary>
    /// GetAllChatMessages.
    /// </summary>
    /// <remarks>Get all chat messages using pagination (for chat members).</remarks>
    /// <param name="chatId">The ID of the chat.</param>
    /// <param name="limit">The maximum number of messages to retrieve.</param>
    /// <param name="nextCursor">The cursor value for pagination.</param>    
    /// <response code="200">Returns a list of chat messages.</response>
    /// <response code="400">Returns a string message if the chat does not exist.</response>
    /// <response code="403">Returns a string message if the user is unauthorized or is not a chat member.</response>
    [HttpGet]
    [Authorize(Roles = "User")]
    [Route("{chatId}/messages")]
    [ProducesResponseType(typeof(List<MessageResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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