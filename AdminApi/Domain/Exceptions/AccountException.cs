using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AccountException : CustomException
    {
        public AccountException(string message) : base(message)
        {
        }

        protected AccountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
