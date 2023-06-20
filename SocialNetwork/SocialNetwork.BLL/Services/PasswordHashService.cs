using System.Text;
using SocialNetwork.BLL.Contracts;

namespace SocialNetwork.BLL.Services;

public class PasswordHashService : IPasswordHashService
{
    public IPasswordHashService.HashSaltPair EncodePassword(string password)
    {
        var salt = "ZHOPA";
        var bytes = Encoding.UTF8.GetBytes(password + salt);

        return new IPasswordHashService.HashSaltPair
        {
            EncodedPassword = bytes,
            Salt = salt
        };
    }
}