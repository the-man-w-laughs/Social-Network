using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Chats;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.Repositories.Chats;

public class ChatRepository : Repository<Chat>, IChatRepository
{
    public ChatRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) { }
}