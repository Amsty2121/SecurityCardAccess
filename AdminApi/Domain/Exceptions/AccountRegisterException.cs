using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class AccountRegisterException : AccountException
    {
        public AccountRegisterException(string message) : base(message)
        {
        }
        protected AccountRegisterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
