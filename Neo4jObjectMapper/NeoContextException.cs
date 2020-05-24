using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Neo4jObjectMapper
{
    [Serializable]
    internal class NeoContextException : Exception
    {
        public NeoContextException()
        {
        }

        public NeoContextException(string message) : base(message)
        {
        }

        public NeoContextException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NeoContextException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
