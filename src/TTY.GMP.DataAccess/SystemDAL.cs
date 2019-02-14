using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.CacheBucket;
using TTY.GMP.Entity.View;
using TTY.GMP.Entity.Web.Common.Request;
using TTY.GMP.ICache;
using TTY.GMP.IDataAccess;
using TTY.GMP.Utility;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 系统统计数据访问
    /// </summary>
    public class SystemDAL : ISystemDAL, IAdoContract
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlHelper"></param>
        public SystemDAL(ISqlHelper sqlHelper)
        {
            this._sqlHelper = sqlHelper;
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="sqlHelper"></param>
        /// <returns></returns>
        public DbConnection GetDbConnection(out ISqlHelper sqlHelper)
        {
            sqlHelper = _sqlHelper;
            return sqlHelper.GetErpSqlConnection();
        }

        /// <summary>
        /// 获取门店数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<int> GetShopCount(GetSystemStatisticsRequest request)
        {
            var sql = string.Format("SELECT COUNT(1) FROM cm_shop where {0}", request.GetQuerySql2());
            var result = await this.ExecuteScalarAsync(sql.ToString());
            return result.ToInt();
        }

        /// <summary>
        /// 获取销售记录数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<int> GetSalesRecordsCount(GetSystemStatisticsRequest request)
        {
            var sql = string.Format("SELECT COUNT(1) from so_retail a inner join cm_shop b on a.shop_id = b.shop_id  where {0}", request.GetQuerySql());
            var result = await this.ExecuteScalarAsync(sql.ToString());
            return result.ToInt();
        }

        /// <summary>
        /// 获取系统统计数据
        /// </summary>
        /// <returns></returns>
        /// <param name="request"></param>
        public async Task<SystemStatisticsView> GetSystemStatistics(GetSystemStatisticsRequest request)
        {
            var shopCount = await GetShopCount(request);
            var salesRecordsCount = await GetSalesRecordsCount(request);
            return new SystemStatisticsView()
            {
                ShopCount = shopCount,
                SalesRecordsCount = salesRecordsCount
            };
        }
    }
}
