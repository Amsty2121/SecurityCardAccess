using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class DeviceNotFoundException : DeviceException
    {
        public DeviceNotFoundException(string message) : base(message)
        { }

        protected DeviceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }

    }
}
