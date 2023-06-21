using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Messages.Request;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Contracts;
using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatsController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IChatRepository _chatRepository;

    public ChatsController(IMapper mapper, IChatRepository chatRepository)
    {
        _mapper = mapper;
        _chatRepository = chatRepository;
    }

    /// <summary>
    /// DeleteChat
    /// </summary>
    /// <remarks>Delete chat. For chat admins.</remarks>      
    [HttpDelete]
    [Authorize(Roles = "Admin, User")]
    [Route("{chatId}")]
    public virtual ActionResult<ChatResponseDto> DeleteChatsChatId([FromRoute][Required] uint chatId)
    {
        // Доступно только для авторизованных пользователей
        // проверяем существует ли чат с таким id если нет выбрасываем Bad request
        // Если запрос кинул админ соц сети то удалеям без вопросов
        // иначе проверяем является ли пользователь создателем чата если да то удаляем
        // если нет выкидываем accessdenied
        return new ChatResponseDto { Id = chatId, CreatedAt = DateTime.Now, Name = "Svin" };
    }

    /// <summary>
    /// DeleteChatMember
    /// </summary>
    /// <remarks>Delete a member from chat (for chat admins, admins).</remarks>         
    [HttpDelete]
    [Authorize(Roles = "Admin, User")]
    [Route("{chatId}/members/{memberId}")]
    public virtual ActionResult<ChatMemberResponseDto> DeleteChatsChatIdMembersParticipantId([FromRoute][Required] uint chatId, [FromRoute][Required] uint memberId)
    {
        // Доступно только для авторизованных пользователей
        // проверяем существует ли чат с таким id если нет выбрасываем Bad request
        // Если запрос кинул админ соц сети то удалеям без вопросов
        // иначе проверяем является ли пользователь админом или создателем чата если да то удаляем
        // если нет выкидываем accessdenied
        return new ChatMemberResponseDto();
    }

    // TODO: make DTO
    /// <summary>
    /// GetAllChatMedias
    /// </summary>
    /// <remarks>Get all chat medias (for chat members).</remarks>    
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    [Route("{chatId}/medias")]
    public virtual IActionResult GetChatChatIdMedias([FromRoute][Required] uint chatId, [FromQuery] uint? limit, [FromQuery] uint? nextCursor)
    {
        // TODO ПРИДУМАТЬ УЖЕ НАКОНЕЦ ТО ЧТО ТО С MEDIA!!!!!!!!!
        return Ok($"GetAllChatMedias");
    }

    // TODO: make DTO
    /// <summary>
    /// GetChatInfo
    /// </summary>
    /// <remarks>Get chat information (name, photo). For chat members.</remarks>         
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    [Route("{chatId}")]
    public virtual IActionResult GetChatsChatId([FromRoute][Required] uint chatId)
    {
        // TODO ПРИДУМАТЬ УЖЕ НАКОНЕЦ ТО ЧТО ТО С MEDIA для фото чата!!!!!!!!!

        return Ok($"GetChatInfo");
    }

    /// <summary>
    /// GetAllChatMembers
    /// </summary>
    /// <remarks>Get all chat members using pagination (for chat members).</remarks>    
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    [Route("{chatId}/members")]
    public virtual ActionResult<List<ChatMemberResponseDto>> GetChatsChatIdMembers([FromRoute][Required] uint chatId, [FromQuery][Required()] uint? limit, [FromQuery] uint? nextCursor)
    {

        // проверяем существует ли такой chatid если нет кидаем Badrequest
        // если запрос кинул админ соц сети то обрабатываем его проверяя параметры пагинации
        // иначе проверяем является ли пользователь участником чата если нет accessdenied
        return new List<ChatMemberResponseDto>() { new ChatMemberResponseDto() };
    }

    /// <summary>
    /// GetAllChatMessages
    /// </summary>
    /// <remarks>Get all chat messages using pagination (for chat members). </remarks>    
    [HttpGet]
    [Route("{chatId}/messages")]
    public virtual ActionResult<List<MessageResponseDto>> GetChatsChatIdMessages([FromRoute][Required] uint chatId, [FromQuery][Required()] uint? limit, [FromQuery] uint? nextCursor)
    {
        return new List<MessageResponseDto>() { new MessageResponseDto() };
    }

    /// <summary>
    /// ChangeChatInfo
    /// </summary>
    /// <remarks>Change chat information (name, photo). For chat admins.</remarks>        
    [HttpPatch]
    [Route("{chatId}")]
    public async virtual Task<ActionResult<ChatResponseDto>> PatchChatsChatId([FromRoute][Required] uint chatId, [FromBody][Required] JsonPatchDocument chatRequestDto)
    {
        return Ok();
    }

    /// <summary>
    /// ChangeChatInfo
    /// </summary>
    /// <remarks>Change chat information (name, photo). For chat admins.</remarks>        
    [HttpPut]
    [Route("{chatId}")]
    public async virtual Task<ActionResult<ChatResponseDto>> PutChatsChatId([FromRoute][Required] uint chatId, [FromBody][Required] ChatRequestDto chatRequestDto)
    {
        var chats = await _chatRepository.SelectAsync((chat) => chat.Id == chatId);
        if (chats.Count == 0)
        {
            return NotFound();
        }

        var existingChat = chats.First();
        _mapper.Map(chatRequestDto, existingChat);

        _chatRepository.Update(existingChat);

        await _chatRepository.SaveAsync();

        return Ok(_mapper.Map<ChatResponseDto>(existingChat));
    }

    /// <summary>
    /// ChangeMemberStatus
    /// </summary>
    /// <remarks>Updates information about chat member (for chat admins, admins).</remarks>    
    [HttpPatch]
    [Route("{chatId}/members/{memberId}")]
    public async virtual Task<ActionResult<ChatMemberResponseDto>> PatchChatsChatIdMembersMemberId([FromRoute][Required] uint chatId, [FromRoute][Required] uint memberId, [FromBody][Required] ChatRequestDto chatMemberStatusDto)
    {
        return Ok();
    }

    /// <summary>
    /// CreateChat
    /// </summary>
    /// <remarks>Create new chat</remarks>        
    [HttpPost]
    public async virtual Task<ActionResult<ChatResponseDto>> PostChats([FromBody][Required] ChatRequestDto chatRequestDto)
    {
        var newChat = await _chatRepository.AddAsync(_mapper.Map<Chat>(chatRequestDto));
        await _chatRepository.SaveAsync();
        return Ok(_mapper.Map<ChatResponseDto>(newChat));
    }

    /// <summary>
    /// AddChatMember
    /// </summary>
    /// <remarks>Add new member to chat (for chat members).</remarks>        
    [HttpPost]
    [Route("{chatId}/members")]
    public virtual ActionResult<ChatMemberResponseDto> PostChatsChatIdMembers([FromRoute][Required] uint chatId, [FromBody][Required] ChatMemberRequestDto postChatMemberDto)
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