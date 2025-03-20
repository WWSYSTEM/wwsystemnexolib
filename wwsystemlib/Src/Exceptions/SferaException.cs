using System;
using System.Runtime.Serialization;

namespace DesktopApp.Exceptions
{
    public class SferaException : Exception
    {
        public SferaException()
        {
        }

        public SferaException(string message) : base(message)
        {
        }

        public SferaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SferaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}