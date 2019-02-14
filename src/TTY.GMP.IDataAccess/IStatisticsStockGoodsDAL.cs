using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.View;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 商品库存统计数据访问
    /// </summary>
    public interface IStatisticsStockGoodsDAL
    {
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
        Task<DateTime> GetLastUpdateTime();

        /// <summary>
        /// 获取地区库存统计
        /// </summary>
        /// <param name="areaIds"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<List<DbStatisticsStockView>> GetStatisticsStock(string areaIds, string level);

        /// <summary>
        /// 获取地区库存统计
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
    }
}
