using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.BLL.Contracts;

public interface IMessageService
{
    Task<Message> AddMessage(Message message);
    Task<Message?> GetMessage(uint messageId);
    Task<bool> IsUserCanDeleteMessage(Message message, uint userId);
    Task DeleteMessage(uint messageId);
    Task AddMessageLike(MessageLike messageLike);
    Task<bool> IsChatMemberAlreadyLikeMessage(uint messageId, uint chatMemberId);
    Task<List<MessageLike>> GetAllMessageLikesPaginated(uint messageId, int limit, int currCursor);
    Task<MessageLike?> DeleteMessageLike(uint messageId, uint chatMemberId);
}