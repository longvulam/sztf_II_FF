using System;
using System.Runtime.Serialization;

namespace sztf_II_FF.Exceptions
{
    [Serializable]
    internal class NincsIlyenJarmuException : Exception
    {
        public NincsIlyenJarmuException()
        {
        }

        public NincsIlyenJarmuException(string message) : base(message)
        {
        }

        public NincsIlyenJarmuException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NincsIlyenJarmuException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}