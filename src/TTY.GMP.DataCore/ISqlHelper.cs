using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace TTY.GMP.DataCore
{
    public interface ISqlHelper
    {
        /// <summary>
        /// 获取gmp项目数据连接
        /// </summary>
        /// <returns></returns>
        DbConnection GetGmpConnection();

        /// <summary>
        /// 获取erp项目数据连接
        /// </summary>
        /// <returns></returns>
        DbConnection GetErpSqlConnection();

        /// <summary>
        /// 执行SQL语句返回对应的查询实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> ExecuteObject<T>(DbConnection connection, string commandText) where T : class;

        /// <summary>
        /// 执行SQL返回第一行第一列结果
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        Task<object> ExecuteScalar(DbConnection connection, string commandText);

        /// <summary>
        /// 执行SQL返回受影响的行数
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        Task<int> Execute(DbConnection connection, string commandText);

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
        Task<Tuple<IEnumerable<T>, int>> ExecutePage<T>(DbConnection connection, int pageIndex, int pageSize, string files, string tableName, string condition, string order) where T : class;
    }
}
