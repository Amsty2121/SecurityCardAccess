﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class SessionIsAlreadyActiveException : SessionException
    {
        public SessionIsAlreadyActiveException(string? message) : base(message)
        {
        }

        protected SessionIsAlreadyActiveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
