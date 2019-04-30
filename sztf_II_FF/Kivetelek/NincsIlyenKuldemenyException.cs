using System;
using System.Runtime.Serialization;

namespace sztf_II_FF.Kivetelek
{
    internal class NincsIlyenKuldemenyException : Exception
    {
        public NincsIlyenKuldemenyException()
        {
        }

        public NincsIlyenKuldemenyException(string message) : base(message)
        {
        }

        public NincsIlyenKuldemenyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NincsIlyenKuldemenyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}