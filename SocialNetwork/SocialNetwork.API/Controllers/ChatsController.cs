using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Messages.Request;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IChatService _chatService;

    public ChatsController(IMapper mapper, IChatService chatService)
    {
        _mapper = mapper;
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
    [Authorize(Roles = "Admin, User")]
    [ProducesResponseType(typeof(ChatResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public virtual async Task<ActionResult<ChatResponseDto>> PostChats([FromBody, Required] ChatRequestDto chatRequestDto)
    {
        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var userId = uint.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        var newChat = new Chat { Name = chatRequestDto.Name, CreatedAt = DateTime.Now};
        var addedChat = await _chatService.AddChat(newChat);

        var chatOwner = new ChatMember
        {
            ChatId = addedChat.Id,
            CreatedAt = DateTime.Now,
            TypeId = ChatMemberType.Owner,
            UserId = userId
        };

        await _chatService.AddChatMember(chatOwner);

        return Ok(_mapper.Map<ChatResponseDto>(addedChat));
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
    [Authorize(Roles = "Admin, User")]
    [Route("{chatId}")]
    [ProducesResponseType(typeof(ChatResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public virtual async Task<ActionResult<ChatResponseDto>> GetChatsChatId([FromRoute, Required] uint chatId)
    {
        var chat = await _chatService.GetChatById(chatId);

        var isChatExisted = chat != null;
        if (!isChatExisted) return BadRequest("Chat with request Id doesn't exist");

        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var userRole = isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;

        var userId = int.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        if (userRole == UserType.User.ToString())
        {
            var isChatMember = await _chatService.IsUserChatMember(chatId, (uint)userId);            
            if (!isChatMember) return Forbid("You are not chat member.");
        }

        return Ok(await _chatService.GetChatById(chatId));
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
    [Authorize(Roles = "Admin, User")]
    [Route("{chatId}/medias")]
    [ProducesResponseType(typeof(List<MediaResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public virtual async Task<ActionResult<List<MediaResponseDto>>> GetChatChatIdMedias(
        [FromRoute, Required] uint chatId,
        [FromQuery, Required] int limit,
        [FromQuery] int nextCursor)
    {
        var chat = await _chatService.GetChatById(chatId);

        var isChatExisted = chat != null;
        if (!isChatExisted) return BadRequest("Chat with request Id doesn't exist");

        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var userRole = isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;

        var userId = int.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        if (userRole == UserType.User.ToString())
        {
            var isChatMember = await _chatService.IsUserChatMember(chatId, (uint)userId);
            if (!isChatMember) return Forbid("You are not chat member.");
        }

        return Ok(await _chatService.GetAllChatMedias(chatId, limit, nextCursor));
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
    [Authorize(Roles = "Admin, User")]
    [Route("{chatId}")]
    [ProducesResponseType(typeof(ChatResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public virtual async Task<ActionResult<ChatResponseDto>> PatchChatsChatId(
        [FromRoute, Required] uint chatId,
        [FromBody, Required] ChatPatchRequestDto chatPatchRequestDto)
    {
        var chat = await _chatService.GetChatById(chatId);

        var isChatExisted = chat != null;
        if (!isChatExisted) return BadRequest("Chat with request Id doesn't exist");

        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var userRole = isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;

        var userId = int.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        if (userRole == UserType.User.ToString())
        {
            var chatOwner = await _chatService.GetChatOwnerByChatId(chatId);
            var isUserChatOwner = chatOwner.UserId == userId;
            if (!isUserChatOwner) return Forbid("You are not chat Owner");
        }
        try
        {
            var updatedChat = await _chatService.ChangeChat(chatId, chatPatchRequestDto);
            return Ok(updatedChat);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

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
    [Authorize(Roles = "Admin, User")]
    [Route("{chatId}")]
    [ProducesResponseType(typeof(ChatResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<ChatResponseDto>> DeleteChatsChatId([FromRoute, Required] uint chatId)
    {
        // Доступно только для авторизованных пользователей
        // проверяем существует ли чат с таким id если нет выбрасываем Bad request
        // Если запрос кинул админ соц сети то удалеям без вопросов
        // иначе проверяем является ли пользователь создателем чата если да то удаляем
        // если нет выкидываем accessdenied

        var chat = await _chatService.GetChatById(chatId);

        var isChatExisted = chat != null;
        if (!isChatExisted) return BadRequest("Chat with request Id doesn't exist");

        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var userRole = isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;

        var userId = int.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        if (userRole == UserType.User.ToString())
        {
            var chatOwner = await _chatService.GetChatOwnerByChatId(chatId);
            var isUserChatOwner = chatOwner.UserId == userId;
            if (!isUserChatOwner) return Forbid("You are not chat Owner");
        }

        await _chatService.DeleteChat(chatId);

        return Ok(_mapper.Map<ChatResponseDto>(chat));
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
    [Route("{chatId}/members")]
    [ProducesResponseType(typeof(ChatMemberResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public virtual async Task<ActionResult<ChatMemberResponseDto>> PostChatsChatIdMembers(
        [FromRoute, Required] uint chatId,
        [FromBody, Required] ChatMemberRequestDto postChatMemberDto)
    {
        var chat = await _chatService.GetChatById(chatId);

        var isChatExisted = chat != null;
        if (!isChatExisted) return BadRequest("Chat with request Id doesn't exist");

        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var userRole = isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;

        var userId = uint.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        if (userRole == UserType.User.ToString())
        {
            var isUserChatMember = await _chatService.IsUserChatMember(chatId, userId);
            if (!isUserChatMember) return Forbid("User isn't chat member");

            var isNewMemberAlreadyInChat = await _chatService.IsUserChatMember(chatId, postChatMemberDto.UserId);
            if (isNewMemberAlreadyInChat) return Conflict("User is already in chat");
        }

        var newChatMember = new ChatMember
        {
            ChatId = chatId,
            CreatedAt = DateTime.Now,
            TypeId = ChatMemberType.Member,
            UserId = postChatMemberDto.UserId
        };

        var addedChatMember = await _chatService.AddChatMember(newChatMember);

        return Ok(_mapper.Map<ChatMemberResponseDto>(addedChatMember));
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
    [Authorize(Roles = "Admin, User")]
    [Route("{chatId}/members")]
    [ProducesResponseType(typeof(List<ChatMemberResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<List<ChatMemberResponseDto>>> GetChatsChatIdMembers(
        [FromRoute, Required] uint chatId,
        [FromQuery, Required] int limit,
        [FromQuery] int nextCursor)
    {
        // проверяем существует ли такой chatid если нет кидаем Badrequest
        // если запрос кинул админ соц сети то обрабатываем его проверяя параметры пагинации
        // иначе проверяем является ли пользователь участником чата если нет accessdenied

        var chat = await _chatService.GetChatById(chatId);

        var isChatExisted = chat != null;
        if (!isChatExisted) return BadRequest("Chat with request Id doesn't exist");

        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var userRole = isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;

        var userId = uint.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        if (userRole == UserType.User.ToString())
        {
            var isUserChatMember = await _chatService.IsUserChatMember(chatId, userId);
            if (!isUserChatMember) return Forbid("User isn't chat member");
        }

        var chatMembers = await _chatService.GetAllChatMembers(chatId, limit, nextCursor);

        return Ok(chatMembers.Select(cm => _mapper.Map<ChatMemberResponseDto>(cm)));
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
    [Route("{chatId}/members/{memberId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<ChangeChatMemberResponseDto>> PatchChatsChatIdMembersMemberId(
        [FromRoute, Required] uint chatId,
        [FromRoute, Required] uint memberId,
        [FromBody, Required] ChangeChatMemberRequestDto changeChatMemberRequestDto)
    {
        var chat = await _chatService.GetChatById(chatId);

        var isChatExisted = chat != null;

        if (!isChatExisted)
        {
            return BadRequest("Chat doesn't exist");
        }

        var isUserChatMember = await _chatService.IsUserChatMember(chatId, memberId);
        
        if (!isUserChatMember)
        {
            return BadRequest("ChatMember doesn't exist");
        }
        
        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var userRole = isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;

        var userId = uint.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        if (userRole == UserType.User.ToString())
        {
            var isUserHaveAdminPermissions = await _chatService.IsUserHaveChatAdminPermissions(chatId, userId);
            switch (changeChatMemberRequestDto.Type)
            {
                
            }
        }                             
        return Ok("ChatMemberStatus");
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
    [Authorize(Roles = "Admin, User")]
    [Route("{chatId}/members/{userToDeleteId}")]
    [ProducesResponseType(typeof(ChatMemberResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult<ChatMemberResponseDto>> DeleteChatsChatIdMembersParticipantId(
        [FromRoute, Required] uint chatId,
        [FromRoute, Required] uint userToDeleteId)
    {
        // Доступно только для авторизованных пользователей
        // проверяем существует ли чат с таким id если нет выбрасываем Bad request
        // Если запрос кинул админ соц сети то удалеям без вопросов
        // иначе проверяем является ли пользователь админом или создателем чата если да то удаляем
        // если нет выкидываем accessdenied

        var chat = await _chatService.GetChatById(chatId);

        var isChatExisted = chat != null;
        if (!isChatExisted) return BadRequest("Chat with request ID doesn't exist");

        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var userRole = isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;

        var userId = uint.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        if (userRole == UserType.User.ToString())
        {
            var isUserHaveAdminPermissions = await _chatService.IsUserHaveChatAdminPermissions(chatId, userId);
            if (!isUserHaveAdminPermissions) return Forbid("User hasn't chat admin permissions");
        }

        var deletedChatMember = await _chatService.DeleteChatMember(chatId, userToDeleteId);
        if (deletedChatMember == null) return NotFound("Chat member not found");

        return Ok(_mapper.Map<ChatMemberResponseDto>(deletedChatMember));
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
    [Route("{chatId}/messages")]
    [ProducesResponseType(typeof(MessageResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<MessageResponseDto>> PostChatsChatIdMessages(
        [FromRoute, Required] uint chatId,
        [FromBody, Required] MessageRequestDto postChatMemberDto)
    {
        var chat = await _chatService.GetChatById(chatId);

        var isChatExisted = chat != null;
        if (!isChatExisted) return BadRequest("Chat with request ID doesn't exist");

        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var userRole = isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;

        var userId = uint.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        if (userRole == UserType.User.ToString())
        {
            var isUserChatMember = await _chatService.IsUserChatMember(chatId, userId);
            if (!isUserChatMember) return Forbid("User isn't chat member");
        }

        var newMessage = new Message
        {
            ChatId = chatId,
            Content = postChatMemberDto.Content,
            CreatedAt = DateTime.Now,
            SenderId = userId
        };

        var addedMessage = await _chatService.AddMessage(newMessage);

        return Ok(_mapper.Map<MessageResponseDto>(addedMessage));
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
    [Authorize(Roles = "Admin, User")]
    [Route("{chatId}/messages")]
    [ProducesResponseType(typeof(List<MessageResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public virtual async Task<ActionResult<List<MessageResponseDto>>> GetChatsChatIdMessages(
        [FromRoute, Required] uint chatId,
        [FromQuery, Required] int limit,
        [FromQuery] int nextCursor)
    {
        // проверяем наличие чата с заданным ID
        // проверяем роль пользователя если админ выполняем запрос
        // если юзер то проверям является ли он участником чата

        var chat = await _chatService.GetChatById(chatId);

        var isChatExisted = chat != null;
        if (!isChatExisted) return BadRequest("Chat with request Id doesn't exist");

        var isUserAuthenticated =
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var userRole = isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;

        var userId = uint.Parse(isUserAuthenticated.Principal!.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType)?.Value!);

        if (userRole == UserType.User.ToString())
        {
            var isUserChatMember = await _chatService.IsUserChatMember(chatId, userId);
            if (!isUserChatMember) return Forbid("User isn't chat member");
        }

        var messages = await _chatService.GetAllChatMessages(chatId, limit, nextCursor);

        return Ok(messages.Select(m => _mapper.Map<MessageResponseDto>(m)));
    }
}