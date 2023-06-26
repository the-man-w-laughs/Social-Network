using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Exceptions
{    
    public class DuplicateEntryException : Exception
    {
        public DuplicateEntryException() { }

        public DuplicateEntryException(string message) : base(message) { }

        public DuplicateEntryException(string message, Exception innerException) : base(message, innerException) { }
    }
}
