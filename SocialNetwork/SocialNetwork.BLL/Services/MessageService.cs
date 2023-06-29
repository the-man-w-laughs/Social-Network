using AutoMapper;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.DTO.Messages.Request;
using SocialNetwork.BLL.DTO.Messages.Response;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Contracts.Chats;
using SocialNetwork.DAL.Contracts.Messages;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.BLL.Services;

public class MessageService : IMessageService
{
    private readonly IMapper _mapper;
    private readonly IMessageRepository _messageRepository;
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly IMessageLikeRepository _messageLikeRepository;

    public MessageService(
        IMapper mapper,
        IMessageRepository messageRepository,
        IChatMemberRepository chatMemberRepository,
        IMessageLikeRepository messageLikeRepository)
    {
        _mapper = mapper;
        _messageRepository = messageRepository;
        _chatMemberRepository = chatMemberRepository;
        _messageLikeRepository = messageLikeRepository;
    }

    #region Public Methods

    public async Task<MessageResponseDto> GetMessage(uint messageId)
    {
        var message = await GetMessageById(messageId);
        return _mapper.Map<MessageResponseDto>(message);
    }

    public async Task<MessageResponseDto> ReplyMessage(uint userId, uint messageId, MessageRequestDto messageRequestDto)
    {
        var message = await GetMessageById(messageId);
        var chatMember = await GetChatMember(userId, message.ChatId);

        var newMessage = new Message
        {
            Content = messageRequestDto.Content,
            CreatedAt = DateTime.Now,
            ChatId = message.ChatId,
            SenderId = chatMember.Id,
            RepliedMessageId = messageId,
        };
        
        var addedMessage = await _messageRepository.AddAsync(newMessage);
        await _messageRepository.SaveAsync();

        return _mapper.Map<MessageResponseDto>(addedMessage);
    }

    public async Task<MessageResponseDto> DeleteMessage(uint userId, uint messageId)
    {
        var message = await GetMessageById(messageId);
        var chatMember = await GetChatMember(userId, message.ChatId);

        var isUserCanDeleteMessage = await IsChatMemberCanDeleteMessage(message, chatMember.Id);
        if (!isUserCanDeleteMessage) 
            throw new AccessDeniedException($"User (ID: {userId}) can't delete message (ID: {messageId})");

        var deletedMessage = await _messageRepository.DeleteById(messageId);
        await _messageRepository.SaveAsync();

        return _mapper.Map<MessageResponseDto>(deletedMessage);
    }

    public async Task<List<MessageLikeResponseDto>> GetAllMessageLikesPaginated(uint messageId, int limit, int currCursor)
    {
        var message = await GetMessageById(messageId);

        return message.MessageLikes.OrderBy(ml => ml.Id)
            .Skip(currCursor)
            .Take(limit)
            .Select(ml => _mapper.Map<MessageLikeResponseDto>(ml))
            .ToList();
    }

    public async Task<MessageLikeResponseDto> LikeMessage(uint userId, uint messageId)
    {
        var message = await GetMessageById(messageId);
        var chatMember = await GetChatMember(userId, message.ChatId);

        var isUserAlreadyLikeMessage = await IsChatMemberAlreadyLikeMessage(message.Id, chatMember.Id);
        if (isUserAlreadyLikeMessage) 
            throw new DuplicateEntryException($"User (ID: {userId}) already like message (ID: {messageId})");

        var messageLike = new MessageLike
        {
            MessageId = messageId,
            CreatedAt = DateTime.Now,
            ChatMemberId = chatMember.Id
        };

        var addedMessageLike = await _messageLikeRepository.AddAsync(messageLike);
        await _messageLikeRepository.SaveAsync();

        return _mapper.Map<MessageLikeResponseDto>(addedMessageLike);
    }

    public async Task<MessageLikeResponseDto> UnlikeMessage(uint userId, uint messageId)
    {
        var message = await GetMessageById(messageId);
        var chatMember = await GetChatMember(userId, message.ChatId);
        
        var isUserAlreadyLikeMessage = await IsChatMemberAlreadyLikeMessage(message.Id, chatMember.Id);
        if (isUserAlreadyLikeMessage) 
            throw new DuplicateEntryException($"User (ID: {userId}) not like message (ID: {messageId})");

        var deletedLike = await DeleteMessageLike(message.Id, chatMember.Id);

        return _mapper.Map<MessageLikeResponseDto>(deletedLike);
    }

    public async Task<MessageResponseDto> ChangeMessage(uint userId, uint messageId, MessagePatchRequestDto messagePatchRequestDto)
    {
        var message = await GetMessageById(messageId);

        if (message.SenderId != userId)
            throw new OwnershipException("Only message sender can change the message.");

        if (messagePatchRequestDto.Content != null)
        {
            message.Content = messagePatchRequestDto.Content;
            message.UpdatedAt = DateTime.Now;
            
            _messageRepository.Update(message);
            await _messageRepository.SaveAsync();
        }
        
        return _mapper.Map<MessageResponseDto>(message);
    }

    #endregion

    #region Private Methods

    private async Task<Message> GetMessageById(uint messageId)
    {
        var message = await _messageRepository.GetByIdAsync(messageId);
        if (message == null)
            throw new NotFoundException($"Message (ID: {messageId}) doesn't exist");
        
        return message;
    }

    private async Task<ChatMember> GetChatMember(uint userId, uint chatId)
    {
        var chatMember = await _chatMemberRepository.GetAsync(
            cm => cm.UserId == userId && cm.ChatId == chatId);

        if (chatMember == null)
            throw new AccessDeniedException($"User (ID: {userId}) isn't chat member of chat (ID: {chatId})");

        return chatMember;
    }

    private async Task<bool> IsChatMemberCanDeleteMessage(Message message, uint chatMemberId)
    {
        if (message.SenderId == chatMemberId) return true;

        var chatMember = await _chatMemberRepository.GetAsync(cm => cm.UserId == chatMemberId);
        return chatMember?.TypeId is ChatMemberType.Admin or ChatMemberType.Owner;
    }

    private async Task<bool> IsChatMemberAlreadyLikeMessage(uint messageId, uint chatMemberId)
    {
        var messageLike = await _messageLikeRepository.GetAsync(
            ml => ml.MessageId == messageId && ml.ChatMemberId == chatMemberId);

        return messageLike != null;
    }

    private async Task<MessageLike?> DeleteMessageLike(uint messageId, uint chatMemberId)
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

    #endregion
}