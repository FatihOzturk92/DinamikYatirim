using Dapper;
using DY.Data.Mapping;
using DY.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DY.Data.Manager.Database
{
    public class Repository<T> where T : class
    {
        #region Constructor
        public Repository()
        {
            DbManager = new DbManager();
        }
        public IEnumerable<T> Query<T>(string sqlString)
        {
            return DbManager.Connection.Query<T>(sqlString, CommandType.Text);
        }
      
        public Repository(DbManager dbManager)
        {
            DbManager = dbManager;
        }
        #endregion
        ~Repository()
        {
            Dispose(false);
        }
        protected bool IsDisposing { get; set; }
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposing)
            {
                IsDisposing = true;
                if (disposing)
                {
                    DbManager.Dispose();
                    DbManager = null;
                }
            }
        }
        public DbManager DbManager { get; set; }

        #region Methods
        public DataSet ExecuteDataSet(string sqlString)
        {
            return ExecuteDataSet(sqlString, CommandType.Text);
        }
        public DataSet ExecuteDataSet(string sqlString, CommandType ctype)
        {
            DbManager.CreateCommand();
            DataSet result = DbManager.ExecuteDataSet(sqlString, ctype);
            return result;
        }

        public DataTable ExecuteDataTable(string sqlString)
        {
            return ExecuteDataTable(sqlString, CommandType.Text);
        }
        public DataTable ExecuteDataTable(string sqlString, CommandType ctype)
        {
            DbManager.CreateCommand();
            DataTable result = DbManager.ExecuteDataTable(sqlString, ctype);
            return result;
        }

  
        #endregion Methods
    }
}
