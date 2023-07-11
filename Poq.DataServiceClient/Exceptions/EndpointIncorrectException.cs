using System.Runtime.Serialization;

namespace Poq.DataSourceClient.Exceptions
{
    [Serializable]
    public class EndpointIncorrectException : Exception
    {
        public EndpointIncorrectException() : base()
        {
        }
        public EndpointIncorrectException(string message) : base(message)
        {
        }
        public EndpointIncorrectException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        protected EndpointIncorrectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}