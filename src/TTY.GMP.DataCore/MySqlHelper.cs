using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Config;
using TTY.GMP.IOC;
using TTY.GMP.Utility;

namespace TTY.GMP.DataCore
{
    /// <summary>
    /// MySql数据访问
    /// </summary>
    public class MySqlHelper : ISqlHelper
    {
        /// <summary>
        /// 获取gmp项目数据连接
        /// </summary>
        /// <returns></returns>
        public DbConnection GetGmpConnection()
        {
            return new MySqlConnection(CustomServiceLocator.GetInstance<IAppConfigurtaionServices>().AppSettings.MySqlGmpConnectionString);
        }

        /// <summary>
        /// 获取erp项目数据连接
        /// </summary>
        /// <returns></returns>
        public DbConnection GetErpSqlConnection()
        {
            return new MySqlConnection(CustomServiceLocator.GetInstance<IAppConfigurtaionServices>().AppSettings.MySqlErpConnectionString);
        }

        /// <summary>
        /// 查询对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteObject<T>(DbConnection connection, string commandText) where T : class
        {
            return await connection.QueryAsync<T>(commandText);
        }

        /// <summary>
        /// 执行SQL返回第一行第一列结果
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public async Task<object> ExecuteScalar(DbConnection connection, string commandText)
        {
            return await connection.ExecuteScalarAsync(commandText);
        }

        /// <summary>
        /// 执行SQL返回受影响的行数
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public async Task<int> Execute(DbConnection connection, string commandText)
        {
            return await connection.ExecuteAsync(commandText);
        }

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="files"></param>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<T>, int>> ExecutePage<T>(DbConnection connection, int pageIndex, int pageSize, string files, string tableName, string condition, string order) where T : class
        {
            var sql = new StringBuilder();
            sql.AppendFormat("SELECT {0} FROM {1} WHERE {2} ORDER BY {3}", files, tableName, condition, order);
            sql.AppendFormat(" LIMIT {0},{1}", (pageIndex - 1) * pageSize, pageSize);
            var strSql = sql.ToString();
            var data = await ExecuteObject<T>(connection, strSql);
            int total;
            if (strSql.ToLower().IndexOf("group by") >= 0)
            {
                strSql = string.Format("SELECT 0 FROM {0} WHERE {1}", tableName, condition);
                strSql = string.Format(" select count(0) from ({0}) as a", strSql);
                total = (await ExecuteScalar(connection, strSql)).ToInt();
            }
            else
            {
                total = (await ExecuteScalar(connection,
                   string.Format("SELECT COUNT(1) FROM {0} WHERE {1}", tableName, condition))).ToInt();
            }
            return Tuple.Create(data, total);
        }
    }
}
