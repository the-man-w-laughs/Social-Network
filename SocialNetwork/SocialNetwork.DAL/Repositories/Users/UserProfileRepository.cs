using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Repositories.Users;

public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
{
    public UserProfileRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}