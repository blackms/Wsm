using System;
using System.Runtime.Serialization;

namespace Wsm.Authentication
{
    [Serializable]
    internal class SignatureVerificationException : Exception
    {
        public SignatureVerificationException()
        {
        }

        public SignatureVerificationException(string message) : base(message)
        {
        }

        public SignatureVerificationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SignatureVerificationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}