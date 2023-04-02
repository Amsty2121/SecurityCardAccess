using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DeviceException : CustomException
    {
        public DeviceException(string message) : base(message)
        {
        }
        protected DeviceException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
