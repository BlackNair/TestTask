using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskConfig.Enums;

namespace TestTaskCore.Interfaces
{
    public interface IDbWorker
    {
        void SetConnect(DbEnum db, ServerEnum server);

        List<T> ExecuteProcGet<T>(string sql, DynamicParameters? param = null);

        void ExecuteProc(string sql, DynamicParameters? param = null);

    }
}
