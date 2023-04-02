using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class CardException : CustomException
    {
        public CardException(string message) : base(message)
        {
        }

        protected CardException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
