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
    /// 零售商品统计
    /// </summary>
    public class StatisticsRetailGoodsDAL : IStatisticsRetailGoodsDAL, IDbContext<GmpDbContext>, IAdoContract
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlHelper"></param>
        public StatisticsRetailGoodsDAL(ISqlHelper sqlHelper)
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
        /// 新增零售商统计信息
        /// </summary>
        /// <param name="model"></param>
        public async Task AddStatisticsRetailGoods(StatisticsRetailGoods model)
        {
            await this.Insert(model);
        }

        /// <summary>
        /// 获取最近统计时间
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetStatisticsRetailGoodsMaxTime()
        {
            var result = await this.ExecuteScalarAsync("select max(StartTime) from statisticsretailgoods");
            return Convert.ToDateTime(result);
        }

        /// <summary>
        /// 获取地区零售统计
        /// </summary>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<List<DbStatisticsRetailView>> GetStatisticsRetail(string areaIds, DateTime startTime, DateTime endTime, string level)
        {
            var areaLevelName = CommonLib.GetAreaLevelFieldName(level);
            var sql = $@"SELECT
                            	{areaLevelName} AS AreaId,
                            	GoodsCategoryId,
                            	GoodsCategoryName,
                            	sum(TotalWeight) AS TotalWeight,
                            	sum(TotalContentsWeight) AS TotalContentsWeight
                            FROM
                            	statisticsretailgoods
                            WHERE
                            	{areaLevelName} IN ({areaIds})
                            AND StartTime >= '{startTime.ToString("yyyy-MM-dd HH:mm:ss")}'
                            AND EndTime <= '{endTime.ToString("yyyy-MM-dd HH:mm:ss")}'
                            GROUP BY
                            	{areaLevelName},
                            	GoodsCategoryId,
                            	GoodsCategoryName";
            var result = await this.ExecuteObjectAsync<DbStatisticsRetailView>(sql);
            if (result != null && result.Any())
            {
                return result.ToList();
            }
            return null;
        }

        /// <summary>
        /// 获取门店零售统计
        /// </summary>
        /// <param name="shops"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<DbStatisticsRetailView>> GetStatisticsRetailByShop(string shops, DateTime startTime, DateTime endTime)
        {
            var sql = $@"SELECT
                            	Street AS AreaId,
                            	GoodsCategoryId,
                            	GoodsCategoryName,
                            	sum(TotalWeight) AS TotalWeight,
                            	sum(TotalContentsWeight) AS TotalContentsWeight
                            FROM
                            	statisticsretailgoods
                            WHERE
                            	ShopId IN ({shops})
                            AND StartTime >= '{startTime.ToString("yyyy-MM-dd HH:mm:ss")}'
                            AND EndTime <= '{endTime.ToString("yyyy-MM-dd HH:mm:ss")}'
                            GROUP BY
                            	Street,
                            	GoodsCategoryId,
                            	GoodsCategoryName";
            var result = await this.ExecuteObjectAsync<DbStatisticsRetailView>(sql);
            if (result != null && result.Any())
            {
                return result.ToList();
            }
            return null;
        }

        /// <summary>
        /// 获取地区门店零售统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<DbStatisticsRetailViewShop>, int>> GetStatisticsRetailShop(RequestPagingBase request, string areaIds, DateTime startTime,
            DateTime endTime, string level)
        {
            var areaLevelName = CommonLib.GetAreaLevelFieldName(level);
            return await this.ExecutePageAsync<DbStatisticsRetailViewShop>(request.PageCurrent, request.PageSize,
                $"ShopId,ShopName,{areaLevelName} AS AreaId,sum(TotalWeight) AS TotalWeight,sum(TotalContentsWeight) AS TotalContentsWeight",
                "statisticsretailgoods",
                $"{areaLevelName} IN ({areaIds}) AND StartTime >= '{startTime.ToString("yyyy-MM-dd HH:mm:ss")}' AND EndTime <= '{endTime.ToString("yyyy-MM-dd HH:mm:ss")}' GROUP BY {areaLevelName},ShopId,ShopName",
                "ShopId desc");
        }

        /// <summary>
        /// 获取门店零售统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="shops"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<DbStatisticsRetailViewShop>, int>> GetStatisticsRetailShopByShop(
            RequestPagingBase request, string shops,
            DateTime startTime, DateTime endTime)
        {
            return await this.ExecutePageAsync<DbStatisticsRetailViewShop>(request.PageCurrent, request.PageSize,
                $"ShopId,ShopName,Street AS AreaId,sum(TotalWeight) AS TotalWeight,sum(TotalContentsWeight) AS TotalContentsWeight",
                "statisticsretailgoods",
                $"ShopId IN ({shops}) AND StartTime >= '{startTime.ToString("yyyy-MM-dd HH:mm:ss")}' AND EndTime <= '{endTime.ToString("yyyy-MM-dd HH:mm:ss")}' GROUP BY Street,ShopId,ShopName",
                "ShopId desc");
        }
    }
}
