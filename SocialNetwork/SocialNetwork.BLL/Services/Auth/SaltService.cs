using SocialNetwork.BLL.Contracts;
using SocialNetwork.DAL;
using BCryptNet = BCrypt.Net.BCrypt;

namespace SocialNetwork.BLL.Services;

public class SaltService : ISaltService
{
    private const int SaltWorkFactor = 8;
    
    public string GenerateSalt() => BCryptNet.GenerateSalt(SaltWorkFactor)[..Constants.SaltMaxLength];
}