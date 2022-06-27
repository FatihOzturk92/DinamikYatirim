using DY.Data.Manager.Database;
using DY.Entity.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DY.Data.Persistence.Plugin
{
    public class RndDataRepository : Repository<RndData>
    {
        public IEnumerable<RndData> GetAll(bool isNoLock = true)
        {
            string sql = string.Format("SELECT * FROM [T_AD_ACCOUNTTYPE] {0} WHERE GCRecId IS NULL", isNoLock ? "WITH (NOLOCK)" : string.Empty);
            return base.Query<RndData>(sql);
        }
        public void Insert(RndData rndData)
        {

            var sql = string.Format("INSERT INTO [DyDatabase].[dbo].[P_RndData] ([CharactersId]) VALUES(" +
                "{0})", rndData.CharactersId);
            base.DbManager.ExecuteNonQuery(sql, System.Data.CommandType.Text);
        }

    }
}
