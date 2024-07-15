using System.Text;
using Konscious.Security.Cryptography;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.DAL;
using BCryptNet = BCrypt.Net.BCrypt;


namespace SocialNetwork.BLL.Services.Auth;

public class PasswordHashService : IPasswordHashService
{
    private const string Pepper = "LG2jj21ma1OPnqizQbjBpq14B1l1zM";
    private const int SaltWorkFactor = 8;
    
    public string GenerateSalt() => BCryptNet.GenerateSalt(SaltWorkFactor)[..Constants.SaltMaxLength];
    
    public byte[] HashPassword(string password, string salt)
    {
        var pepperedPassword = password + Pepper;
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(pepperedPassword));
        argon2.Salt = Encoding.UTF8.GetBytes(salt);
        argon2.DegreeOfParallelism = 8; 
        argon2.MemorySize = 65536; 
        argon2.Iterations = 4; 
        return argon2.GetBytes(32); 
    }

    public bool IsPasswordValid(string password)
    {
        if (password.Length < Constants.UserPasswordMinLength)
            return false;

        if (password.Any(char.IsWhiteSpace))
            return false;

        return true;
    }

    public bool VerifyPassword(string password,string salt, byte[] hashedPassword)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password + Pepper));
        argon2.Salt = Encoding.UTF8.GetBytes(salt);
        argon2.DegreeOfParallelism = 8; 
        argon2.MemorySize = 65536; 
        argon2.Iterations = 4; 
        var newHash = argon2.GetBytes(hashedPassword.Length);
        return hashedPassword.SequenceEqual(newHash);
    }
}