using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.View;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 商品采购统计数据访问
    /// </summary>
    public class StatisticsPurchaseGoodsDAL : IStatisticsPurchaseGoodsDAL, IDbContext<GmpDbContext>, IAdoContract
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlHelper"></param>
        public StatisticsPurchaseGoodsDAL(ISqlHelper sqlHelper)
        {
            this._sqlHelper = sqlHelper;
        }

        /// <summary>
        /// 获取数据访问上下文
        /// </summary>
        /// <returns></returns>
        public GmpDbContext GetDbContext()
        {
            return new GmpDbContext();
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="sqlHelper"></param>
        /// <returns></returns>
        public DbConnection GetDbConnection(out ISqlHelper sqlHelper)
        {
            sqlHelper = _sqlHelper;
            return _sqlHelper.GetGmpConnection();
        }

        /// <summary>
        /// 添加商品采购统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddStatisticsPurchaseGoods(StatisticsPurchaseGoods model)
        {
            await this.Insert(model);
        }

        /// <summary>
        /// 获取最近统计时间
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetStatisticsPurchaseGoodsMaxTime()
        {
            var result = await this.ExecuteScalarAsync("select max(StartTime) from statisticspurchasegoods");
            return Convert.ToDateTime(result);
        }

        /// <summary>
        /// 获取地区采购统计
        /// </summary>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<List<DbStatisticsPurchaseView>> GetStatisticsPurchase(string areaIds, DateTime startTime,
             DateTime endTime, string level)
        {
            var areaLevelName = CommonLib.GetAreaLevelFieldName(level);
            var sql = $@"SELECT
                            	{areaLevelName} AS AreaId,
                            	GoodsCategoryId,
                            	GoodsCategoryName,
                            	sum(TotalWeight) AS TotalWeight,
                            	sum(TotalContentsWeight) AS TotalContentsWeight
                            FROM
                            	statisticspurchasegoods
                            WHERE
                            	{areaLevelName} IN ({areaIds})
                            AND StartTime >= '{startTime.ToString("yyyy-MM-dd HH:mm:ss")}'
                            AND EndTime <= '{endTime.ToString("yyyy-MM-dd HH:mm:ss")}'
                            GROUP BY
                            	{areaLevelName},
                            	GoodsCategoryId,
                            	GoodsCategoryName";
            var result = await this.ExecuteObjectAsync<DbStatisticsPurchaseView>(sql);
            if (result != null && result.Any())
            {
                return result.ToList();
            }
            return null;
        }

        /// <summary>
        /// 获取地区采购统计
        /// </summary>
        /// <param name="shops"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<DbStatisticsPurchaseView>> GetStatisticsPurchaseByShop(string shops, DateTime startTime,
            DateTime endTime)
        {
            var sql = $@"SELECT
                            	Street AS AreaId,
                            	GoodsCategoryId,
                            	GoodsCategoryName,
                            	sum(TotalWeight) AS TotalWeight,
                            	sum(TotalContentsWeight) AS TotalContentsWeight
                            FROM
                            	statisticspurchasegoods
                            WHERE
                            	ShopId IN ({shops})
                            AND StartTime >= '{startTime.ToString("yyyy-MM-dd HH:mm:ss")}'
                            AND EndTime <= '{endTime.ToString("yyyy-MM-dd HH:mm:ss")}'
                            GROUP BY
                            	Street,
                            	GoodsCategoryId,
                            	GoodsCategoryName";
            var result = await this.ExecuteObjectAsync<DbStatisticsPurchaseView>(sql);
            if (result != null && result.Any())
            {
                return result.ToList();
            }
            return null;
        }

        /// <summary>
        /// 获取地区门店采购统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<DbStatisticsPurchaseShopView>, int>> GetStatisticsPurchaseShop(RequestPagingBase request,
            string areaIds,
            DateTime startTime,
            DateTime endTime, string level)
        {
            var areaLevelName = CommonLib.GetAreaLevelFieldName(level);
            return await this.ExecutePageAsync<DbStatisticsPurchaseShopView>(request.PageCurrent, request.PageSize,
                $"ShopId,ShopName,{areaLevelName} AS AreaId,sum(TotalWeight) AS TotalWeight,sum(TotalContentsWeight) AS TotalContentsWeight",
                "statisticspurchasegoods",
                $"{areaLevelName} IN ({areaIds}) AND StartTime >= '{startTime.ToString("yyyy-MM-dd HH:mm:ss")}' AND EndTime <= '{endTime.ToString("yyyy-MM-dd HH:mm:ss")}' GROUP BY {areaLevelName},ShopId,ShopName",
                "ShopId desc");
        }

        /// <summary>
        /// 获取地区门店采购统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="shops"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<DbStatisticsPurchaseShopView>, int>> GetStatisticsPurchaseShopByShop(
            RequestPagingBase request, string shops,
            DateTime startTime, DateTime endTime)
        {
            return await this.ExecutePageAsync<DbStatisticsPurchaseShopView>(request.PageCurrent, request.PageSize,
                $"ShopId,ShopName,Street AS AreaId,sum(TotalWeight) AS TotalWeight,sum(TotalContentsWeight) AS TotalContentsWeight",
                "statisticspurchasegoods",
                $"ShopId IN ({shops}) AND StartTime >= '{startTime.ToString("yyyy-MM-dd HH:mm:ss")}' AND EndTime <= '{endTime.ToString("yyyy-MM-dd HH:mm:ss")}' GROUP BY Street,ShopId,ShopName",
                "ShopId desc");
        }
    }
}
