using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.DataCore;
using System.Linq;
using System.Threading.Tasks;

namespace TTY.GMP.DataAccess.Common
{
    /// <summary>
    /// 扩展ado.net功能
    /// </summary>
    internal static class AdoExtensions
    {
        /// <summary>
        /// 执行SQL语句返回对应的查询实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        internal static async Task<IEnumerable<T>> ExecuteObjectAsync<T>(this IAdoContract @this, string commandText) where T : class
        {
            using (var con = @this.GetDbConnection(out ISqlHelper sqlHelper))
            {
                return await sqlHelper.ExecuteObject<T>(con, commandText);
            }
        }

        /// <summary>
        /// 执行SQL返回第一行第一列结果
        /// </summary>
        /// <param name="this"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        internal static async Task<object> ExecuteScalarAsync(this IAdoContract @this, string commandText)
        {
            using (var con = @this.GetDbConnection(out ISqlHelper sqlHelper))
            {
                return await sqlHelper.ExecuteScalar(con, commandText);
            }
        }

        /// <summary>
        /// 执行SQL返回受影响的行数
        /// </summary>
        /// <param name="this"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        internal static async Task<int> ExecuteAsync(this IAdoContract @this, string commandText)
        {
            using (var con = @this.GetDbConnection(out ISqlHelper sqlHelper))
            {
                return await sqlHelper.Execute(con, commandText);
            }
        }

        /// <summary>
        /// 分页获取数据信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="pageCurrent"></param>
        /// <param name="pageSize"></param>
        /// <param name="files"></param>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        internal static async Task<Tuple<IEnumerable<T>, int>> ExecutePageAsync<T>(this IAdoContract @this, int pageCurrent, int pageSize, string files, string tableName, string condition, string order) where T : class
        {
            using (var con = @this.GetDbConnection(out ISqlHelper sqlHelper))
            {
                return await sqlHelper.ExecutePage<T>(con, pageCurrent, pageSize, files, tableName, condition, order);
            }
        }
    }
}
