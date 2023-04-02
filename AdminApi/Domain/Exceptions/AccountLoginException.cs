using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class AccountLoginException : AccountException
    {
        public AccountLoginException(string message) : base(message)
        {

        }
        protected AccountLoginException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
