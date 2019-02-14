using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 采购单据数数据访问
    /// </summary>
    public interface IStatisticsPurchaseCountDAL
    {
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
    }
}
