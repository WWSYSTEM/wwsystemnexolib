using System;
using System.Runtime.Serialization;

namespace DesktopApp.Exceptions
{
    public class ActionResultException : Exception
    {
        public ActionResultException()
        {
        }

        public ActionResultException(string message) : base(message)
        {
        }

        public ActionResultException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ActionResultException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}