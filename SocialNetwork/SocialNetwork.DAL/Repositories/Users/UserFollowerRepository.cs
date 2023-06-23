using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Repositories.Users;

public class UserFollowerRepository : Repository<UserFollower>, IUserFollowerRepository
{
    public UserFollowerRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}