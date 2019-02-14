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
    /// 商品采购统计数据访问
    /// </summary>
    public interface IStatisticsPurchaseGoodsDAL
    {
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
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<DbStatisticsPurchaseShopView>, int>> GetStatisticsPurchaseShop(RequestPagingBase request, string areaIds,
            DateTime startTime,
            DateTime endTime, string level);

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
    }
}
