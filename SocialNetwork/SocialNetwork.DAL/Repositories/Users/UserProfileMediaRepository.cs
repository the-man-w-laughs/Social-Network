using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Repositories.Users;

public class UserProfileMediaRepository : Repository<UserProfileMedia>, IUserProfileMediaRepository
{
    public UserProfileMediaRepository(SocialNetworkContext socialNetworkContext) : base(socialNetworkContext) {}
}