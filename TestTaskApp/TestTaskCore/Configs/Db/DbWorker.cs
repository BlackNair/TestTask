using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskConfig.Enums;
using TestTaskCore.Interfaces;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace TestTaskCore.Configs.Db
{
    public class DbWorker: IDbWorker
    {

        private static string _connection = "Server=ServerOne;Initial Catalog=DatabaseOne;Trusted_Connection=True";

        public void SetConnect(DbEnum db, ServerEnum server)
        {
            _connection = $"Server={server};Initial Catalog={db};TrustedConnection=True";
        }

        public List<T> ExecuteProcGet<T>(string sql, DynamicParameters? param = null)
        {
            var result = new List<T>();

            using (var connect = new SqlConnection(_connection))
            {
                connect.Open();
                var transaction = connect.BeginTransaction();
                try
                {
                    result = connect.Query<T>(sql, param, transaction, commandTimeout: 36000, commandType: CommandType.StoredProcedure).ToList();
                    transaction.Commit();
                }
                catch (Exception ex) 
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                { 
                    connect.Close();
                }
            }
            return result;
        }

        public void ExecuteProc(string sql, DynamicParameters? param = null)
        {
            using (var connect = new SqlConnection(_connection))
            {
                connect.Open();
                var transaction = connect.BeginTransaction();
                try
                {
                    connect.ExecuteReader(sql, param, transaction, commandTimeout: 36000, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    connect.Close();
                }
            }
            return;
        }

    }
}
