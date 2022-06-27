using DY.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DY.ExceptionHandling
{
    public abstract class DYException : System.Exception, ISerializable
    {
         public int ErrorId { get; set; }

        public string ErrorCode { get; set; }

        protected DYException()
            : base() { }

        protected DYException(string message)
            : base(message) { }

        protected DYException(string format, params object[] args)
            : base(string.Format( format,args)) { }

        protected DYException(string errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        protected DYException(string message, System.Exception inner)
            : base(message, inner) { }

        protected DYException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public abstract void Log(ILog logger, string message);
    }
}
