using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.DAL.Contracts.Chats;

public interface IChatMemberRepository : IRepository<ChatMember>
{
    Task<ChatMember?> GetChatMember(uint chatId, uint userId);
    Task<ChatMember?> DeleteChatMember(uint chatId, uint userId);
}