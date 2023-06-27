namespace SocialNetwork.BLL.Exceptions;

public class LoggedInUserAccessException : Exception
{
    public LoggedInUserAccessException(string message) : base(message) { }
}