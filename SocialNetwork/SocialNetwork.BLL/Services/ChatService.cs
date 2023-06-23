using SocialNetwork.BLL.Contracts;
using SocialNetwork.DAL.Contracts.Chats;
using SocialNetwork.DAL.Contracts.Messages;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.BLL.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IChatMemberRepository _chatMemberRepository;

    public ChatService(IChatRepository chatRepository, IChatMemberRepository chatMemberRepository, IMessageRepository messageRepository)
    {
        _chatRepository = chatRepository;
        _chatMemberRepository = chatMemberRepository;
        _messageRepository = messageRepository;
    }

    public async Task<Chat?> GetChatById(uint chatId)
    {
        return await _chatRepository.GetByIdAsync(chatId);
    }

    public async Task<ChatMember> GetChatOwnerByChatId(uint chatId)
    {
        return (await _chatMemberRepository.GetAsync(
            cm => cm.ChatId == chatId && cm.TypeId == ChatMemberType.Owner))!;
    }

    public async Task DeleteChat(uint chatId)
    {
        await _chatRepository.DeleteById(chatId);
        await _chatRepository.SaveAsync();
    }

    public async Task<bool> IsUserHaveChatAdminPermissions(uint chatId, uint userId)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        var chatMember = chat?.ChatMembers.FirstOrDefault(cm => cm.UserId == userId);
        return chatMember?.TypeId is ChatMemberType.Admin or ChatMemberType.Owner;
    }

    public async Task<ChatMember?> DeleteChatMember(uint chatId, uint userId)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        var memberToDelete = chat?.ChatMembers.FirstOrDefault(cm => cm.UserId == userId);
        if (memberToDelete != null)
        {
            await _chatMemberRepository.DeleteById(memberToDelete.Id);
            await _chatMemberRepository.SaveAsync();
        }
        return memberToDelete;
    }

    public async Task<bool> IsUserChatMember(uint chatId, uint userId)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        var chatMember = chat?.ChatMembers.FirstOrDefault(cm => cm.UserId == userId);
        return chatMember != null;
    }

    public async Task<ChatMember?> GetChatMember(uint chatId, uint userId)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        var chatMember = chat?.ChatMembers.FirstOrDefault(cm => cm.UserId == userId);
        return chatMember;
    }

    public async Task<List<ChatMember>> GetAllChatMembers(uint chatId, int limit, int currCursor)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        return chat!.ChatMembers.OrderBy(cm => cm.Id)
            .Skip(currCursor)
            .Take(limit)
            .ToList();
    }

    public async Task<List<Message>> GetAllChatMessages(uint chatId, int limit, int nextCursor)
    {
        var chat = await _chatRepository.GetByIdAsync(chatId);
        return chat!.Messages.OrderBy(m => m.Id)
            .Where(p => p.Id > nextCursor)
            .Take(limit)
            .ToList();
    }
    public async Task<Chat> AddChat(Chat newChat)
    {
        var chat = await _chatRepository.AddAsync(newChat);
        await _chatRepository.SaveAsync();
        return chat;
    }

    public async Task<ChatMember> AddChatMember(ChatMember chatMember)
    {
        var newChatMember = await _chatMemberRepository.AddAsync(chatMember);
        await _chatMemberRepository.SaveAsync();
        return newChatMember;
    }

    public async Task<Message> AddMessage(Message message)
    {
        var newMessage = await _messageRepository.AddAsync(message);
        await _messageRepository.SaveAsync();
        return newMessage;
    }
}