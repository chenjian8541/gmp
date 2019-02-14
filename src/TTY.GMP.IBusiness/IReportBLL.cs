using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.View;

namespace TTY.GMP.IBusiness
{
    /// <summary>
    /// 报表业务访问
    /// </summary>
    public interface IReportBLL
    {
        /// <summary>
        /// 新增零售商统计信息
        /// </summary>
        /// <param name="model"></param>
        Task AddStatisticsRetailGoods(StatisticsRetailGoods model);

        /// <summary>
        /// 获取最近统计时间
        /// </summary>
        /// <returns></returns>
        Task<DateTime> GetStatisticsRetailGoodsMaxTime();

        /// <summary>
        /// 添加商品采购统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddStatisticsPurchaseGoods(StatisticsPurchaseGoods model);

        /// <summary>
        /// 获取最近统计时间
        /// </summary>
        /// <returns></returns>
        Task<DateTime> GetStatisticsPurchaseGoodsMaxTime();

        /// <summary>
        /// 保存商品库存统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SaveStatisticsStockGoods(StatisticsStockGoods model);

        /// <summary>
        /// 获取最后一次更新时间
        /// </summary>
        /// <returns></returns>
        Task<DateTime> GetStockGoodsLastUpdateTime();

        /// <summary>
        /// 获取地区零售统计
        /// </summary>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<List<DbStatisticsRetailView>> GetStatisticsRetail(string areaIds, DateTime startTime, DateTime endTime, string level);

        /// <summary>
        /// 获取地区门店零售统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<DbStatisticsRetailViewShop>, int>> GetStatisticsRetailShop(RequestPagingBase request, string areaIds,
            DateTime startTime,
            DateTime endTime, string level);

        /// <summary>
        /// 获取门店零售统计
        /// </summary>
        /// <param name="shops"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<List<DbStatisticsRetailView>> GetStatisticsRetailByShop(string shops, DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取门店零售统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="shops"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<DbStatisticsRetailViewShop>, int>> GetStatisticsRetailShopByShop(
            RequestPagingBase request, string shops,
            DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取地区采购统计
        /// </summary>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<List<DbStatisticsPurchaseView>> GetStatisticsPurchase(string areaIds, DateTime startTime,
            DateTime endTime, string level);

        /// <summary>
        /// 获取地区门店采购统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<DbStatisticsPurchaseShopView>, int>> GetStatisticsPurchaseShop(RequestPagingBase request, string areaIds,
            DateTime startTime,
            DateTime endTime, string level);

        /// <summary>
        /// 获取地区采购统计
        /// </summary>
        /// <param name="shops"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<List<DbStatisticsPurchaseView>> GetStatisticsPurchaseByShop(string shops, DateTime startTime,
            DateTime endTime);

        /// <summary>
        /// 获取地区门店采购统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="shops"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<DbStatisticsPurchaseShopView>, int>> GetStatisticsPurchaseShopByShop(RequestPagingBase request, string shops,
            DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取地区库存统计
        /// </summary>
        /// <param name="areaIds"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<List<DbStatisticsStockView>> GetStatisticsStock(string areaIds, string level);

        /// <summary>
        /// 获取门店库存统计
        /// </summary>
        /// <param name="shops"></param>
        /// <returns></returns>
        Task<List<DbStatisticsStockView>> GetStatisticsStockByShop(string shops);

        /// <summary>
        /// 获取地区门店的库存统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="areaIds"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<DbStatisticsStockViewShop>, int>> GetStatisticsStockShop(RequestPagingBase request, string areaIds, string level);

        /// <summary>
        /// 获取地区门店的库存统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="shops"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<DbStatisticsStockViewShop>, int>> GetStatisticsStockShopByShop(RequestPagingBase request, string shops);

        /// <summary>
        /// 获取门店销售单数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<List<StatisticsRetailCount>> GetStatisticsRetailCount(string shopIds, DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取地区销售单数量
        /// </summary>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <param name="limitShops"></param>
        /// <returns></returns>
        Task<List<StatisticsRetailCount>> GetStatisticsRetailCount(string areaIds, DateTime startTime, DateTime endTime, string level, string limitShops);

        /// <summary>
        /// 获取门店采购单数量
        /// </summary>
        /// <param name="shopIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<List<StatisticsPurchaseCount>> GetStatisticsPurchaseCount(string shopIds, DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取地区采购单数量
        /// </summary>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <param name="limitShops"></param>
        /// <returns></returns>
        Task<List<StatisticsPurchaseCount>> GetStatisticsPurchaseCount(string areaIds, DateTime startTime, DateTime endTime, string level, string limitShops);

        /// <summary>
        /// 新增采购单 单据统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddStatisticsPurchaseCount(StatisticsPurchaseCount model);

        /// <summary>
        /// 获取最近统计时间
        /// </summary>
        /// <returns></returns>
        Task<DateTime> GetStatisticsPurchaseCountMaxTime();

        /// <summary>
        /// 新增销售单数据统计
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddStatisticsRetailCount(StatisticsRetailCount model);

        /// <summary>
        /// 获取最近统计时间
        /// </summary>
        /// <returns></returns>
        Task<DateTime> GetStatisticsRetailCountMaxTime();
    }
}
