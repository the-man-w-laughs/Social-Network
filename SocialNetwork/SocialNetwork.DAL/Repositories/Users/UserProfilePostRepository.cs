using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Repositories.Users;

public class UserProfilePostRepository : Repository<UserProfilePost>, IUserProfilePostRepository
{
    public UserProfilePostRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}