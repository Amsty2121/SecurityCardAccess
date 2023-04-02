using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class SessionException : CustomException
    {
        public SessionException(string message) : base(message)
        {
        }

        protected SessionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
