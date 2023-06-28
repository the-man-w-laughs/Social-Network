namespace SocialNetwork.BLL.Exceptions
{    
    public class OwnershipException : Exception
    {
        public OwnershipException() { }

        public OwnershipException(string message) : base(message) { }

        public OwnershipException(string message, Exception innerException) : base(message, innerException) { }
    }
}
