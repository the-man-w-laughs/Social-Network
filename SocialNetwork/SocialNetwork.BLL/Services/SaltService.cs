using System.Text;
using SocialNetwork.BLL.Contracts;
using BCryptNet = BCrypt.Net.BCrypt;

namespace SocialNetwork.BLL.Services;

public class SaltService : ISaltService
{
    private const int SaltLength = 20;
    public string GenerateSalt()
    {
        return BCryptNet.GenerateSalt(SaltLength)[..20];
    }
}