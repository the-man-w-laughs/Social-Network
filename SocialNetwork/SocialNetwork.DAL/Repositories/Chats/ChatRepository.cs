using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Chats;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.Repositories.Chats;

public class ChatRepository : Repository<Chat>, IChatRepository
{
    public ChatRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {        
    }

    public async Task<ChatMember> GetChatOwnerId(uint chatId)
    {
        return (await SocialNetworkContext.ChatMembers
            .FirstOrDefaultAsync(cm => cm.ChatId == chatId && cm.TypeId == ChatMemberType.Owner))!;
    }

    public async Task DeleteChatById(uint chatId)
    {
        var deletedChat = await SocialNetworkContext.Chats.FindAsync(chatId);
        if (deletedChat != null) SocialNetworkContext.Chats.Remove(deletedChat);
    }

    public async Task<ChatMember?> GetChatMember(uint chatId, uint userId)
    {
        return await SocialNetworkContext.ChatMembers.FirstOrDefaultAsync(
            cm => cm.ChatId == chatId && cm.UserId == userId);
    }

    public async Task<ChatMember?> DeleteChatMember(uint chatId, uint userId)
    {
        var deletedChatMember = await GetChatMember(chatId, userId);
        if (deletedChatMember != null) SocialNetworkContext.ChatMembers.Remove(deletedChatMember);
        return deletedChatMember;
    }

    public async Task<List<Message>> GetAllMessages(uint chatId)
    {
        return await SocialNetworkContext.Messages.Where(m=> m.ChatId == chatId).ToListAsync();
    }

    public async Task<ChatMember> AddChatMember(ChatMember chatMember)
    {
        await SocialNetworkContext.ChatMembers.AddAsync(chatMember);
        await SocialNetworkContext.SaveChangesAsync();
        return chatMember;
    }

    public async Task<Message> AddMessage(Message newMessage)
    {
        await SocialNetworkContext.Messages.AddAsync(newMessage);
        return newMessage;
    }
}