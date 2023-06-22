using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.Contracts.Chats;

public interface IChatRepository : IRepository<Chat>
{
    Task<List<Message>> GetAllMessages(uint chatId);
}