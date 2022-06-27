using DY.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DY.Data.Manager.Database
{
    public   class DbManager : IDisposable
    {
        #region Constructor

        public DbManager()
        {
            Connection = new SqlConnection(ConnectionString);
        }

        #endregion

        #region Properties

        private static string _connectionString;
        private string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = ConfigurationUtility.Instance.GetConnectionString(DbConnectionName.DBCONNECTION_DEFAULT);
                }
                return _connectionString;
            }
        }

        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public IList<IDbDataParameter> Parameters { get; set; }
        public IDbCommand Command { get; set; }
        #endregion 

        #region Method

        public void Dispose()
        {
            if (Connection != null)
                Connection.Close();
        }

        public void OpenConnection()
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
        }

        public void CloseConnection()
        {
            if (Transaction == null)
                Connection.Close();
        }

        public void BeginTrans()
        {
            if (Connection != null)
            {
                if (Connection.State == ConnectionState.Closed)
                    Connection.Open();
                Transaction = Connection.BeginTransaction();
            }
        }

        public void CommitTrans()
        {
            if (Connection != null && Transaction != null)
            {
                Transaction.Commit();
                Transaction = null;
                CloseConnection();
            }
        }

        public void RollbackTrans()
        {
            if (Connection != null && Transaction != null)
            {
                Transaction.Rollback();
                Transaction = null;
                CloseConnection();
            }
        }
        public abstract System.Data.IDbDataAdapter CreateAdapter();

        public DataSet ExecuteDataSet(string sqlString, CommandType ctype)
        {
            var dataSet = new DataSet();
            try
            {
                OpenConnection();
                PrepareCommand(sqlString, ctype);
                var dataAdapter = this.CreateAdapter();
                dataAdapter.SelectCommand = Command;

                dataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                this.ThrowException(ex);
            }
            finally
            {
                Command.Dispose();
                CloseConnection();
            }
            return dataSet;
        }
        private void PrepareCommand(string sqlstring, CommandType commandType)
        {
            for (int i = 0; i < this.Parameters.Count; i++)
            {
                this.Command.Parameters.Add(this.Parameters[i]);
            }

            this.Parameters.Clear();

            Command.CommandType = commandType;
            Command.CommandText = sqlstring;
            Command.Connection = Connection;
            Command.Transaction = Transaction;
        }
        protected virtual void ThrowException(Exception ex)
        {
            if (this.Command != null)
                throw new DY.ExceptionHandling.DbException(string.Format("Query :{0} Ex :{1}", this.Command.CommandText, ex));
            throw new DY.ExceptionHandling.DbException(ex.Message);
        }

        public System.Data.IDbCommand CreateCommand()
        {
            this.Command = CreateCommandInternal();
            return this.Command;
        }
        public DataTable ExecuteDataTable(string sqlString, CommandType ctype)
        {
            var dataSet = new DataSet();
            try
            {
                OpenConnection();
                PrepareCommand(sqlString, ctype);
                var dataAdapter = this.CreateAdapter();
                dataAdapter.SelectCommand = Command;

                dataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                this.ThrowException(ex);
            }
            finally
            {
                Command.Dispose();
                CloseConnection();
            }
            return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : new DataTable();
        }


        public int ExecuteNonQuery(string sqlString, CommandType ctype)
        {
            int result = 0;
            try
            {
                OpenConnection();
                PrepareCommand(sqlString, ctype);
                result = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.ThrowException(ex);
            }
            finally
            {
                Command.Dispose();
                CloseConnection();
            }
            return result;
        }

        public void ExecuteNonQuery(string sqlString, CommandType ctype, out object returnValue)
        {
            returnValue = null;
            try
            {

                OpenConnection();
                PrepareCommandWithoutClearParameter(sqlString, ctype);
                Command.ExecuteNonQuery();
                this.Parameters.Clear();

                returnValue = (Command.Parameters["@return"] as DbParameter).Value;
            }
            catch (Exception ex)
            {
                this.ThrowException(ex);
            }
            finally
            {
                Command.Dispose();
                CloseConnection();
            }
        }
        private void PrepareCommandWithoutClearParameter(string sqlstring, CommandType commandType)
        {
            for (int i = 0; i < this.Parameters.Count; i++)
            {
                this.Command.Parameters.Add(this.Parameters[i]);
            }
            //TT 28.11.2015 return parametresini almayı engelliyor execute sonrasına taşıdım burdaki satırı
            //this.Parameters.Clear();

            Command.CommandType = commandType;
            Command.CommandText = sqlstring;
            Command.Connection = Connection;
            Command.Transaction = Transaction;
        }
        public object ExecuteScalar(string sqlString, CommandType ctype)
        {
            object result = DBNull.Value;
            try
            {
                OpenConnection();
                PrepareCommand(sqlString, ctype);
                result = Command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                this.ThrowException(ex);
            }
            finally
            {
                Command.Dispose();
                CloseConnection();
            }
            return result;
        }
        #endregion
    }
}
