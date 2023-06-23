using SocialNetwork.BLL.Contracts;
using SocialNetwork.DAL.Contracts.Chats;
using SocialNetwork.DAL.Contracts.Messages;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.BLL.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly IMessageLikeRepository _messageLikeRepository;

    public MessageService(IMessageRepository messageRepository, IChatMemberRepository chatMemberRepository, IMessageLikeRepository messageLikeRepository)
    {
        _messageRepository = messageRepository;
        _chatMemberRepository = chatMemberRepository;
        _messageLikeRepository = messageLikeRepository;
    }

    public async Task<Message> AddMessage(Message message)
    {
        var addedMessage = await _messageRepository.AddAsync(message);
        await _messageRepository.SaveAsync();
        return addedMessage;
    }

    public async Task<Message?> GetMessage(uint messageId)
    {
        return await _messageRepository.GetByIdAsync(messageId);
    }
    
    public async Task<bool> IsUserCanDeleteMessage(Message message, uint userId)
    {
        if (message.SenderId == userId) return true;

        var chatMember = await _chatMemberRepository.GetAsync(cm => cm.UserId == userId);
        return chatMember?.TypeId is ChatMemberType.Admin or ChatMemberType.Owner;
    }

    public async Task DeleteMessage(uint messageId)
    {
        await _messageRepository.DeleteById(messageId);
        await _messageRepository.SaveAsync();
    }

    public async Task AddMessageLike(MessageLike messageLike)
    {
        await _messageLikeRepository.AddAsync(messageLike);
        await _messageLikeRepository.SaveAsync();
    }

    public async Task<bool> IsChatMemberAlreadyLikeMessage(uint messageId, uint chatMemberId)
    {
        var messageLike = await _messageLikeRepository.GetAsync(
            ml => ml.MessageId == messageId && ml.ChatMemberId == chatMemberId);

        return messageLike != null;
    }

    public async Task<List<MessageLike>> GetAllMessageLikesPaginated(uint messageId, int limit, int currCursor)
    {
        var message = await _messageRepository.GetByIdAsync(messageId);
        return message!.MessageLikes.OrderBy(ml => ml.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
    }

    public async Task<MessageLike?> DeleteMessageLike(uint messageId, uint chatMemberId)
    {
        var messageLike = await _messageLikeRepository.GetAsync(
            ml => ml.MessageId == messageId && ml.ChatMemberId == chatMemberId);
        
        if (messageLike != null)
        {
            _messageLikeRepository.Delete(messageLike);
            await _messageLikeRepository.SaveAsync();
        }

        return messageLike;
    }
}