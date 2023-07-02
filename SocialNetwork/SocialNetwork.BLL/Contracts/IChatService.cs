using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Messages.Request;
using SocialNetwork.BLL.DTO.Messages.Response;

namespace SocialNetwork.BLL.Contracts;

public interface IChatService
{
    Task<ChatResponseDto> CreateChat(uint userId, ChatPostDto chatRequestDto);
    Task<ChatResponseDto> GetChatInfo(uint chatId, uint userId);
    Task<List<MediaResponseDto>> GetChatMedias(uint userId, uint chatId, int limit, int nextCursor);
    Task<ChatResponseDto> UpdateChat(uint chatId, uint userId, ChatPatchDto chatPatchRequestDto);
    Task<ChatResponseDto> DeleteChat(uint chatId, uint userId);
    Task<ChatMemberResponseDto> AddChatMember(uint userId, uint chatId, uint userToAddId);
    Task<List<ChatMemberResponseDto>> GetChatMembers(uint userId, uint chatId, int limit, int nextCursor);
    Task<ChatMemberResponseDto> UpdateChatMember(uint chatId, uint userId,uint memberId,
        ChatMemberPutDto changeChatMemberRequestDto);
    Task<ChatMemberResponseDto> DeleteChatMember(uint userId, uint userToDeleteId, uint chatId);
    Task<List<MessageResponseDto>> GetChatMessages(uint chatId, uint userId, int limit, int nextCursor);
}