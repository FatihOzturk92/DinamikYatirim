using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DY.Data.Mapping
{
    internal static class IDataReaderExtensions
    {
        public static string[] GetColumnNames(this IDataReader reader)
        {
            var table = reader
                .GetSchemaTable();
            var result = new string[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                result[i] = table.Rows[i][0] as string;
            }
            return result;
        }
    }
}
