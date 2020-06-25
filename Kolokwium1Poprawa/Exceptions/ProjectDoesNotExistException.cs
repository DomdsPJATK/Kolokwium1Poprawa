using System;
using System.Runtime.Serialization;

namespace Kolokwium1Poprawa.Exceptions
{
    public class ProjectDoesNotExistException : Exception
    {
        public ProjectDoesNotExistException()
        {
        }

        protected ProjectDoesNotExistException(SerializationInfo? info, StreamingContext context) : base(info, context)
        {
        }

        public ProjectDoesNotExistException(string? message) : base(message)
        {
        }

        public ProjectDoesNotExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}