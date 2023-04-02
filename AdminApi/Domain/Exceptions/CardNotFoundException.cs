using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class CardNotFoundException : CardException
    {
        public CardNotFoundException(string message) : base(message)
        { }

        protected CardNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }

    }
}
