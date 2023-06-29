namespace SocialNetwork.BLL.Exceptions;

public class WrongCredentialsException : Exception
{
    public WrongCredentialsException(string message) : base(message) { }
}