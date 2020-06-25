using System;
using System.Runtime.Serialization;

namespace Kolokwium1Poprawa.Exceptions
{
    public class MemberNotExistException : Exception
    {
        public MemberNotExistException()
        {
        }

        protected MemberNotExistException(SerializationInfo? info, StreamingContext context) : base(info, context)
        {
        }

        public MemberNotExistException(string? message) : base(message)
        {
        }

        public MemberNotExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}