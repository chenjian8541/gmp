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
    /// 零售商品统计
    /// </summary>
    public interface IStatisticsRetailGoodsDAL
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
        /// 获取地区零售统计
        /// </summary>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<List<DbStatisticsRetailView>> GetStatisticsRetail(string areaIds, DateTime startTime,
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
        /// 获取地区门店零售统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="areaIds"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<DbStatisticsRetailViewShop>, int>> GetStatisticsRetailShop(RequestPagingBase request, string areaIds,
            DateTime startTime, DateTime endTime, string level);

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
    }
}
