using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DeviceAlreadyExistsException : DeviceException
    {
        public DeviceAlreadyExistsException(string? message) : base(message)
        {
        }

        protected DeviceAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
