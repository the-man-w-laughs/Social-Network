using SocialNetwork.DAL.Contracts.Base;
using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.DAL.Contracts;

public interface IChatRepository : IRepository<Chat>
{
    public Task<ChatMember> GetChatOwnerId(uint chatId);

    public Task DeleteChatById(uint chatId);

    public Task<ChatMember?> GetChatMember(uint chatId, uint userId);
    Task<ChatMember?> DeleteChatMember(uint chatId, uint userId);
}