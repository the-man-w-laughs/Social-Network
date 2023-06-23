using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Messages;
using SocialNetwork.DAL.Entities.Messages;

namespace SocialNetwork.DAL.Repositories.Messages;

public class MessageLikeRepository : Repository<MessageLike>, IMessageLikeRepository
{
    public MessageLikeRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}