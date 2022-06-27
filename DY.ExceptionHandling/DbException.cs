using DY.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DY.ExceptionHandling
{
    public class DbException : DYException, ISerializable
    {
        public DbException()
            : base() { }

        public DbException(string message)
            : base(message) { }

        public DbException(string message, System.Exception innerException)
            : base(message, innerException) { }

        protected DbException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public override void Log(ILog logger, string message)
        {
            logger.Log(LogLevel.Error, message);
        }
    }
}
