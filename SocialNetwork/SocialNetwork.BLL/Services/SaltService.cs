using SocialNetwork.BLL.Contracts;
using BCryptNet = BCrypt.Net.BCrypt;

namespace SocialNetwork.BLL.Services;

public class SaltService : ISaltService
{
    private const int SaltWorkFactor = 8;
    public string GenerateSalt()
    {
        return BCryptNet.GenerateSalt(SaltWorkFactor)[..20];
    }
}