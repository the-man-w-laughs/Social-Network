using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.DAL.Contracts;
using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.BLL.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;

    public ChatService(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Chat?> GetChatById(uint chatId)
    {
        var chats = await _chatRepository.SelectAsync(c => c.Id == chatId);
        return chats.FirstOrDefault();
    }

    public async Task<ChatMember> GetChatOwnerByChatId(uint chatId)
    {
        return await _chatRepository.GetChatOwnerId(chatId);
    }

    public async Task DeleteChat(uint chatId)
    {
        await _chatRepository.DeleteChatById(chatId);
    }

    public async Task<bool> IsUserHaveAdminPermissions(uint chatId, uint userId)
    {
        var chatMember = await _chatRepository.GetChatMember(chatId, userId);
        return chatMember?.TypeId is ChatMemberType.Admin or ChatMemberType.Owner;
    }

    public async Task<ChatMember?> DeleteChatMember(uint chatId, uint userId)
    {
        return await _chatRepository.DeleteChatMember(chatId, userId);
    }

    public async Task<bool> IsUserChatMember(uint chatId, uint userId)
    {
        var chatMember = await _chatRepository.GetChatMember(chatId, userId);
        return chatMember != null;
    }

    public async Task<List<ChatMember>> GetAllChatMembers(uint chatId, int limit, int currCursor)
    {
        var chat = (await _chatRepository.SelectAsync(c => c.Id == chatId)).FirstOrDefault();
        return chat!.ChatMembers
            .OrderBy(cm=> cm.Id)
            .Where(p=>p.Id>currCursor)
            .Take(limit)
            .ToList();
    }
}