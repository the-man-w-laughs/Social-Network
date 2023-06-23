using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Chats;
using SocialNetwork.DAL.Entities.Chats;

namespace SocialNetwork.DAL.Repositories.Chats;

public class ChatMemberRepository : Repository<ChatMember>, IChatMemberRepository
{
    public ChatMemberRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}