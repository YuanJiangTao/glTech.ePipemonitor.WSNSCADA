using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PluginContract.Data
{
    public interface IDapper
    {
        int BulkInsert(DataTable datasource, string destinationTableName);
        int BulkUpdate(DataTable datasource, string destinationTableName, params string[] keys);

        IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    }
}
