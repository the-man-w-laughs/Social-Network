using SocialNetwork.BLL.DTO.Chats.Request;
using SocialNetwork.BLL.DTO.Chats.Response;
using SocialNetwork.BLL.DTO.Medias.Response;
using SocialNetwork.BLL.DTO.Messages.Request;
using SocialNetwork.BLL.DTO.Messages.Response;

namespace SocialNetwork.BLL.Contracts;

public interface IChatService
{
    Task<ChatResponseDto> CreateChat(ChatRequestDto chatRequestDto, uint userId);
    Task<ChatResponseDto> GetChatInfo(uint chatId, uint userId);
    Task<List<MediaResponseDto>> GetChatMedias(uint userId, uint chatId, int limit, int nextCursor);
    Task<ChatResponseDto> UpdateChat(uint chatId, uint userId, ChatPatchRequestDto chatPatchRequestDto);
    Task<ChatResponseDto> DeleteChat(uint chatId, uint userId);
    Task<ChatMemberResponseDto> AddChatMember(uint userId, uint chatId, ChatMemberRequestDto postChatMemberDto);
    Task<List<ChatMemberResponseDto>> GetChatMembers(uint userId, uint chatId, int limit, int nextCursor);
    Task<ChatMemberResponseDto> UpdateChatMember(uint chatId, uint userId,uint memberId,
        ChangeChatMemberRequestDto changeChatMemberRequestDto);
    Task<ChatMemberResponseDto> DeleteChatMember(uint userId, uint userToDeleteId, uint chatId);
    Task<MessageResponseDto> SendMessage(uint chatId, uint userId, MessageRequestDto postChatMemberDto);
    Task<List<MessageResponseDto>> GetChatMessages(uint chatId, uint userId, int limit, int nextCursor);
}