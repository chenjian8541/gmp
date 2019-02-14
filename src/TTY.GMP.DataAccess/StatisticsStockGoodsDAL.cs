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
using TTY.GMP.Entity.View;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 商品库存统计数据访问
    /// </summary>
    public class StatisticsStockGoodsDAL : IStatisticsStockGoodsDAL, IDbContext<GmpDbContext>, IAdoContract
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        private ISqlHelper _sqlHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlHelper"></param>
        public StatisticsStockGoodsDAL(ISqlHelper sqlHelper)
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
        /// 保存商品库存统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SaveStatisticsStockGoods(StatisticsStockGoods model)
        {
            var oldStockRecord = await this.Find<GmpDbContext, StatisticsStockGoods>(p =>
                 p.OrgId == model.OrgId && p.ShopId == model.ShopId && p.GoodsId == model.GoodsId);
            if (oldStockRecord != null)
            {
                oldStockRecord.GoodsCategoryId = model.GoodsCategoryId;
                oldStockRecord.Province = model.Province;
                oldStockRecord.City = model.City;
                oldStockRecord.District = model.District;
                oldStockRecord.Street = model.Street;
                oldStockRecord.ShopName = model.ShopName;
                oldStockRecord.GoodsName = model.GoodsName;
                oldStockRecord.GoodsSpec = model.GoodsSpec;
                oldStockRecord.GoodsContents = model.GoodsContents;
                oldStockRecord.GoodsCategoryName = model.GoodsCategoryName;
                oldStockRecord.TotalCount = model.TotalCount;
                oldStockRecord.TotalWeight = model.TotalWeight;
                oldStockRecord.TotalContentsWeight = model.TotalContentsWeight;
                oldStockRecord.UpdateTime = model.UpdateTime;
                await this.Update(oldStockRecord);
            }
            await this.Insert(model);
        }

        /// <summary>
        /// 获取最后一次更新时间
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetLastUpdateTime()
        {
            var result = await this.ExecuteScalarAsync("SELECT max(UpdateTime) FROM StatisticsStockGoods");
            return Convert.ToDateTime(result);
        }

        /// <summary>
        /// 获取地区库存统计
        /// </summary>
        /// <param name="areaIds"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<List<DbStatisticsStockView>> GetStatisticsStock(string areaIds, string level)
        {
            var areaLevelName = CommonLib.GetAreaLevelFieldName(level);
            var sql = $@"SELECT
                            	{areaLevelName} AS AreaId,
                            	GoodsCategoryId,
                            	GoodsCategoryName,
                            	sum(TotalWeight) AS TotalWeight,
                            	sum(TotalContentsWeight) AS TotalContentsWeight
                            FROM
                            	statisticsstockgoods
                            WHERE
                            	{areaLevelName} IN ({areaIds}) and TotalCount < 5000
                            GROUP BY
                            	{areaLevelName},
                            	GoodsCategoryId,
                            	GoodsCategoryName";
            var result = await this.ExecuteObjectAsync<DbStatisticsStockView>(sql);
            if (result != null && result.Any())
            {
                return result.ToList();
            }
            return null;
        }

        /// <summary>
        /// 获取地区库存统计
        /// </summary>
        /// <param name="shops"></param>
        /// <returns></returns>
        public async Task<List<DbStatisticsStockView>> GetStatisticsStockByShop(string shops)
        {
            var sql = $@"SELECT
                            	Street AS AreaId,
                            	GoodsCategoryId,
                            	GoodsCategoryName,
                            	sum(TotalWeight) AS TotalWeight,
                            	sum(TotalContentsWeight) AS TotalContentsWeight
                            FROM
                            	statisticsstockgoods
                            WHERE
                            	ShopId IN ({shops}) and TotalCount < 5000
                            GROUP BY
                            	Street,
                            	GoodsCategoryId,
                            	GoodsCategoryName";
            var result = await this.ExecuteObjectAsync<DbStatisticsStockView>(sql);
            if (result != null && result.Any())
            {
                return result.ToList();
            }
            return null;
        }

        /// <summary>
        /// 获取地区门店的库存统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="areaIds"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<DbStatisticsStockViewShop>, int>> GetStatisticsStockShop(RequestPagingBase request,
            string areaIds, string level)
        {
            var areaLevelName = CommonLib.GetAreaLevelFieldName(level);
            return await this.ExecutePageAsync<DbStatisticsStockViewShop>(request.PageCurrent, request.PageSize,
                $"ShopId,ShopName,{areaLevelName} AS AreaId,sum(TotalWeight) AS TotalWeight,sum(TotalContentsWeight) AS TotalContentsWeight",
                "statisticsstockgoods",
                $"{areaLevelName} IN ({areaIds}) and TotalCount < 5000 GROUP BY {areaLevelName},ShopId,ShopName",
                "ShopId desc");
        }

        /// <summary>
        /// 获取地区门店的库存统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="shops"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<DbStatisticsStockViewShop>, int>> GetStatisticsStockShopByShop(RequestPagingBase request,
             string shops)
        {
            return await this.ExecutePageAsync<DbStatisticsStockViewShop>(request.PageCurrent, request.PageSize,
                $"ShopId,ShopName,Street AS AreaId,sum(TotalWeight) AS TotalWeight,sum(TotalContentsWeight) AS TotalContentsWeight",
                "statisticsstockgoods",
                $"ShopId IN ({shops}) and TotalCount < 5000 GROUP BY Street,ShopId,ShopName",
                "ShopId desc");
        }
    }
}
