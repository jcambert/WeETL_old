using System;
using System.Runtime.Serialization;

namespace WeETL.Core
{
    public sealed class ConnectorException : Exception
    {
        public ConnectorException()
        {
        }

        public ConnectorException(string message) : base(message)
        {
        }

        public ConnectorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ConnectorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
