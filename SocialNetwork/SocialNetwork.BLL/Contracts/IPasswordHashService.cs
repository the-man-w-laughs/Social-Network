namespace SocialNetwork.BLL.Contracts;

public interface IPasswordHashService
{
    public byte[] HashPassword(string password,string salt);

    public bool VerifyPassword(string password,string salt, byte[] hashedPassword);

}