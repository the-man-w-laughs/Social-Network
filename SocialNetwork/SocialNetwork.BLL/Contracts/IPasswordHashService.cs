namespace SocialNetwork.BLL.Contracts;

public interface IPasswordHashService
{
    public struct HashSaltPair
    {
        public byte[] EncodedPassword;
        public string Salt;
    }
    
    HashSaltPair EncodePassword(string password);
}