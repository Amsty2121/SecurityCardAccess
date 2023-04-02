using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    public class AccountNotFoundException : AccountException
    {
        public AccountNotFoundException(string message) : base(message)
        { }

        protected AccountNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }

    }
}
