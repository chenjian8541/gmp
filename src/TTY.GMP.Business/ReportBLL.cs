using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.View;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 报表业务访问
    /// </summary>
    public class ReportBLL : IReportBLL
    {
        /// <summary>
        /// 零售商品统计数据访问
        /// </summary>
        private readonly IStatisticsRetailGoodsDAL _statisticsRetailGoodsDal;

        /// <summary>
        /// 商品采购统计数据访问
        /// </summary>
        private readonly IStatisticsPurchaseGoodsDAL _statisticsPurchaseGoodsDal;

        /// <summary>
        /// 商品库存统计数据访问
        /// </summary>
        private readonly IStatisticsStockGoodsDAL _statisticsStockGoodsDal;

        /// <summary>
        /// 销售单据数 数据访问
        /// </summary>
        private readonly IStatisticsRetailCountDAL _statisticsRetailCountDAL;

        /// <summary>
        /// 采购单据数 数据访问
        /// </summary>
        private readonly IStatisticsPurchaseCountDAL _statisticsPurchaseCountDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="statisticsRetailGoodsDal"></param>
        /// <param name="statisticsPurchaseGoodsDal"></param>
        /// <param name="statisticsStockGoodsDal"></param>
        /// <param name="statisticsRetailCountDal"></param>
        /// <param name="statisticsPurchaseCountDal"></param>
        public ReportBLL(IStatisticsRetailGoodsDAL statisticsRetailGoodsDal, IStatisticsPurchaseGoodsDAL statisticsPurchaseGoodsDal,
            IStatisticsStockGoodsDAL statisticsStockGoodsDal, IStatisticsRetailCountDAL statisticsRetailCountDal,
            IStatisticsPurchaseCountDAL statisticsPurchaseCountDal)
        {
            this._statisticsRetailGoodsDal = statisticsRetailGoodsDal;
            this._statisticsPurchaseGoodsDal = statisticsPurchaseGoodsDal;
            this._statisticsStockGoodsDal = statisticsStockGoodsDal;
            this._statisticsRetailCountDAL = statisticsRetailCountDal;
            this._statisticsPurchaseCountDal = statisticsPurchaseCountDal;
        }

        /// <summary>
        /// 新增零售商统计信息
        /// </summary>
        /// <param name="model"></param>
        public async Task AddStatisticsRetailGoods(StatisticsRetailGoods model)
        {
            await this._statisticsRetailGoodsDal.AddStatisticsRetailGoods(model);
        }

        /// <summary>
        /// 获取最近统计时间
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetStatisticsRetailGoodsMaxTime()
        {
            return await this._statisticsRetailGoodsDal.GetStatisticsRetailGoodsMaxTime();
        }

        /// <summary>
        /// 添加商品采购统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddStatisticsPurchaseGoods(StatisticsPurchaseGoods model)
        {
            await this._statisticsPurchaseGoodsDal.AddStatisticsPurchaseGoods(model);
        }

        /// <summary>
        /// 获取最近统计时间
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetStatisticsPurchaseGoodsMaxTime()
        {
            return await this._statisticsPurchaseGoodsDal.GetStatisticsPurchaseGoodsMaxTime();
        }

        /// <summary>
        /// 保存商品库存统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SaveStatisticsStockGoods(StatisticsStockGoods model)
        {
            await this._statisticsStockGoodsDal.SaveStatisticsStockGoods(model);
        }

        /// <summary>
        /// 获取最后一次更新时间
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetStockGoodsLastUpdateTime()
        {
            return await this._statisticsStockGoodsDal.GetLastUpdateTime();
        }

        /// <summary>
        /// 获取地区零售统计
        /// </summary>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<List<DbStatisticsRetailView>> GetStatisticsRetail(string areaIds, DateTime startTime, DateTime endTime,
            string level)
        {
            return await this._statisticsRetailGoodsDal.GetStatisticsRetail(areaIds, startTime, endTime, level);
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
        public async Task<Tuple<IEnumerable<DbStatisticsRetailViewShop>, int>> GetStatisticsRetailShop(RequestPagingBase request,
            string areaIds,
            DateTime startTime,
            DateTime endTime, string level)
        {
            return await this._statisticsRetailGoodsDal.GetStatisticsRetailShop(request, areaIds, startTime, endTime, level);
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
            return await this._statisticsRetailGoodsDal.GetStatisticsRetailByShop(shops, startTime, endTime);
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
            return await this._statisticsRetailGoodsDal.GetStatisticsRetailShopByShop(request, shops, startTime,
                endTime);
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
            return await this._statisticsPurchaseGoodsDal.GetStatisticsPurchase(areaIds, startTime, endTime, level);
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
            return await this._statisticsPurchaseGoodsDal.GetStatisticsPurchaseShop(request, areaIds, startTime,
                endTime, level);
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
            return await this._statisticsPurchaseGoodsDal.GetStatisticsPurchaseByShop(shops, startTime, endTime);
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
            return await this._statisticsPurchaseGoodsDal.GetStatisticsPurchaseShopByShop(request, shops, startTime,
                endTime);
        }

        /// <summary>
        /// 获取地区库存统计
        /// </summary>
        /// <param name="areaIds"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<List<DbStatisticsStockView>> GetStatisticsStock(string areaIds, string level)
        {
            return await this._statisticsStockGoodsDal.GetStatisticsStock(areaIds, level);
        }

        /// <summary>
        /// 获取地区库存统计
        /// </summary>
        /// <param name="shops"></param>
        /// <returns></returns>
        public async Task<List<DbStatisticsStockView>> GetStatisticsStockByShop(string shops)
        {
            return await this._statisticsStockGoodsDal.GetStatisticsStockByShop(shops);
        }

        /// <summary>
        /// 获取地区门店的库存统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="areaIds"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<DbStatisticsStockViewShop>, int>> GetStatisticsStockShop(
            RequestPagingBase request, string areaIds, string level)
        {
            return await this._statisticsStockGoodsDal.GetStatisticsStockShop(request, areaIds, level);
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
            return await this._statisticsStockGoodsDal.GetStatisticsStockShopByShop(request, shops);
        }

        /// <summary>
        /// 获取门店销售单数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<StatisticsRetailCount>> GetStatisticsRetailCount(string shopIds, DateTime startTime, DateTime endTime)
        {
            return await _statisticsRetailCountDAL.GetStatisticsRetailCount(shopIds, startTime, endTime);
        }

        /// <summary>
        /// 获取地区销售单数量
        /// </summary>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <param name="limitShops"></param>
        /// <returns></returns>
        public async Task<List<StatisticsRetailCount>> GetStatisticsRetailCount(string areaIds, DateTime startTime, DateTime endTime,
            string level, string limitShops)
        {
            return await _statisticsRetailCountDAL.GetStatisticsRetailCount(areaIds, startTime, endTime, level, limitShops);
        }

        /// <summary>
        /// 获取门店采购单数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<StatisticsPurchaseCount>> GetStatisticsPurchaseCount(string shopIds, DateTime startTime, DateTime endTime)
        {
            return await _statisticsPurchaseCountDal.GetStatisticsPurchaseCount(shopIds, startTime, endTime);
        }

        /// <summary>
        /// 获取地区采购单数量
        /// </summary>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <param name="limitShops"></param>
        /// <returns></returns>
        public async Task<List<StatisticsPurchaseCount>> GetStatisticsPurchaseCount(string areaIds, DateTime startTime,
             DateTime endTime, string level, string limitShops)
        {
            return await _statisticsPurchaseCountDal.GetStatisticsPurchaseCount(areaIds, startTime, endTime, level, limitShops);
        }

        /// <summary>
        /// 新增采购单 单据统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddStatisticsPurchaseCount(StatisticsPurchaseCount model)
        {
            await this._statisticsPurchaseCountDal.AddStatisticsPurchaseCount(model);
        }

        /// <summary>
        /// 获取最近统计时间
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetStatisticsPurchaseCountMaxTime()
        {
            return await this._statisticsPurchaseCountDal.GetStatisticsPurchaseCountMaxTime();
        }

        /// <summary>
        /// 新增销售单数据统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddStatisticsRetailCount(StatisticsRetailCount model)
        {
            await this._statisticsRetailCountDAL.AddStatisticsRetailCount(model);
        }

        /// <summary>
        /// 获取最近统计时间
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetStatisticsRetailCountMaxTime()
        {
            return await this._statisticsRetailCountDAL.GetStatisticsRetailCountMaxTime();
        }
    }
}
