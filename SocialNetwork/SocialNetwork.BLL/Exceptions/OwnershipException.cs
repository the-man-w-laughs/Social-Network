using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Exceptions
{    
    public class OwnershipException : Exception
    {
        public OwnershipException() { }

        public OwnershipException(string message) : base(message) { }

        public OwnershipException(string message, Exception innerException) : base(message, innerException) { }
    }
}
