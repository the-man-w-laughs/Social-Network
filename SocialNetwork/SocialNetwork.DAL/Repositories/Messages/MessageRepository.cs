using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Messages;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.Repositories.Messages;


public class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}