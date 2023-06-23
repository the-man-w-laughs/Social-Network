using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Messages;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.Repositories.Messages;

public class MessageMediaRepository : Repository<MessageMedia>, IMessageMediaRepository
{
    public MessageMediaRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}