using System.Runtime.Serialization;

namespace Poq.DataSourceClient.Exceptions
{
    [Serializable]
    public class MockyUnavailableException : Exception
    {
        public MockyUnavailableException() : base()
        {
        }
        public MockyUnavailableException(string message) : base(message)
        {
        }
        public MockyUnavailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        protected MockyUnavailableException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}