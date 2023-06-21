using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Users;
using SocialNetwork.DAL.Repositories.Base;

namespace SocialNetwork.DAL.Repositories;

public class ChatRepository : Repository<Chat>, IChatRepository
{
    public ChatRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {        
    }
}