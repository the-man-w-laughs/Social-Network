namespace SocialNetwork.BLL.Contracts;

public interface IPasswordHashService
{
    string GenerateSalt();
    byte[] HashPassword(string password,string salt);
    bool VerifyPassword(string password,string salt, byte[] hashedPassword);
    bool IsPasswordValid(string password);
}