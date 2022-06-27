using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DY.Data.Mapping
{
    internal class DataRecordHelper
    {
        public MethodInfo OrdinalMethod { get; private set; }

        public MethodInfo IsNullMethod { get; private set; }

        public MethodInfo ValueMethod { get; private set; }
        public DataRecordHelper()
        {
            var methods = typeof(IDataRecord).GetMethods();
            this.OrdinalMethod = methods.First(p => p.Name == "GetOrdinal");
            this.IsNullMethod = methods.First(p => p.Name == "IsDBNull");
            this.ValueMethod = methods.First(p => p.Name == "GetValue");
        }
    }
}