using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.BLL.Contracts;

public interface IChatService
{
    public Task<Chat?> GetChatById(uint chatId);

    public Task<ChatMember> GetChatOwnerByChatId(uint chatId);

    public Task DeleteChat(uint chatId);

    public Task<bool> IsUserHaveAdminPermissions(uint chatId, uint userId);

    public Task<ChatMember?> DeleteChatMember(uint chatId, uint userId);

    public Task<bool> IsUserChatMember(uint chatId, uint userId);
    public Task<List<ChatMember>> GetAllChatMembers(uint chatId, int limit, int currCursor);
}