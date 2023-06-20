using System.Text;
using SocialNetwork.BLL.Contracts;

namespace SocialNetwork.BLL.Services;

public class SaltService : ISaltService
{
    public string GenerateSalt()
    {
        const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        const int saltLength = 20;

        var random = new Random();
        var salt = new StringBuilder();

        for (var i = 0; i < saltLength; i++)
        {
            var randomIndex = random.Next(allowedChars.Length);
            salt.Append(allowedChars[randomIndex]);
        }

        return salt.ToString();
    }
}