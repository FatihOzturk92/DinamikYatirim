using DY.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DY.ExceptionHandling
{
    public class CoreException : DYException
    {
        public CoreException()
            : base() { }

        public CoreException(string message)
            : base(message) { }

        public CoreException(string message, System.Exception innerException)
            : base(message, innerException) { }

        protected CoreException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public override void Log(ILog logger, string message)
        {
            logger.Log(LogLevel.Error, message);
        }
    }
}
