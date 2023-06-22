using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Chats;
using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.DAL.Repositories.Chats;

public class ChatMemberRepository : Repository<ChatMember>, IChatMemberRepository
{
    public ChatMemberRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
    
    public async Task<ChatMember?> GetChatMember(uint chatId, uint userId)
    {
        return await SocialNetworkContext.ChatMembers.FirstOrDefaultAsync(
            cm => cm.ChatId == chatId && cm.UserId == userId);
    }
    
    public async Task<ChatMember?> DeleteChatMember(uint chatId, uint userId)
    {
        var userToDelete = await GetChatMember(chatId, userId);
        if (userToDelete != null) SocialNetworkContext.ChatMembers.Remove(userToDelete);
        return userToDelete;
    }
}