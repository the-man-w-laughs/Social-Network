using DocumentFormat.OpenXml.Office2010.PowerPoint;
using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.BLL.Contracts;

public interface IChatService
{
    Task<ChatResponseDto?> GetChatById(uint chatId);
    Task<ChatMember> GetChatOwnerByChatId(uint chatId);
    Task DeleteChat(uint chatId);
    Task<bool> IsUserHaveChatAdminPermissions(uint chatId, uint userId);
    Task<ChatMember?> DeleteChatMember(uint chatId, uint userId);
    Task<bool> IsUserChatMember(uint chatId, uint userId);
    Task<List<ChatMember>> GetAllChatMembers(uint chatId, int limit, int currCursor);                     
    Task<ChatMember?> GetChatMember(uint chatId, uint userId);    
    Task<List<Message>> GetAllChatMessages(uint chatId, int limit, int nextCursor);
    Task<List<MediaResponseDto>> GetAllChatMedias(uint chatId, int limit, int nextCursor);
    Task<Chat> AddChat(Chat newChat);
    Task<ChatMember> AddChatMember(ChatMember chatOwner);
    Task<Message> AddMessage(Message message);
    Task<ChatResponseDto> ChangeChat(uint chatId, ChatPatchRequestDto chatRequestDto);
}