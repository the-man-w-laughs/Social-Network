using System.Runtime.Intrinsics.Arm;
using System.Text;
using Konscious.Security.Cryptography;
using SocialNetwork.BLL.Contracts;
using BCryptNet = BCrypt.Net.BCrypt;


namespace SocialNetwork.BLL.Services;

public class PasswordHashService : IPasswordHashService
{
    private const string Pepper = "LG2jj21ma1OPnqizQbjBpq14B1l1zM";
    public byte[] HashPassword(string password, string salt)
    {
        var pepperedPassword = password + Pepper;
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(pepperedPassword));
        argon2.Salt = Encoding.UTF8.GetBytes(salt);
        argon2.DegreeOfParallelism = 8; // Adjust as needed
        argon2.MemorySize = 65536; // Adjust as needed
        argon2.Iterations = 4; // Adjust as needed

        return argon2.GetBytes(32); // 32 bytes = 256 bits
    }

    public bool VerifyPassword(string password, byte[] hashedPassword)
    {
        var pepperedPassword = password + Pepper;
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(pepperedPassword));

        return true;
        //return argon2.Verify(hashedPassword);
    }
}