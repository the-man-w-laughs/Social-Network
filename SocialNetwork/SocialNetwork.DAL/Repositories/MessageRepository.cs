using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts;
using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Repositories.Base;

namespace SocialNetwork.DAL.Repositories;


public class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}