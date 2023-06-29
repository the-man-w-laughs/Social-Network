using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Repositories.Users;

public class UserFriendRepository : Repository<UserFriend>, IUserFriendRepository
{
    public UserFriendRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}