using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class SessionIsAlreadyClosedException : SessionException
    {
        public SessionIsAlreadyClosedException(string? message) : base(message)
        {
        }

        protected SessionIsAlreadyClosedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
