using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class SessionNotFoundException : SessionException
    {
        public SessionNotFoundException(string message) : base(message)
        { }

        protected SessionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
