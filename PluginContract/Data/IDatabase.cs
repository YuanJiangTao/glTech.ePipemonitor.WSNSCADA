using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PluginContract.Data
{
    public interface IDatabase
    {
        /// <summary>
        /// 批量更新操作
        /// </summary>
        int BulkUpdate(DataTable datasource, string destinationTableName, params string[] keys);

        /// <summary>
        /// 批量插入操作.
        /// </summary>
        int BulkInsert(DataTable datasource, string destinationTableName);


        int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        //IDataReader ExecuteReader(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        //IEnumerable<TReturn> Query<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        //List<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        string QueryJson(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
    }
}
