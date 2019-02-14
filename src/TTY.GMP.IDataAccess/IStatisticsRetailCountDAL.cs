using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 销售单据数数据访问
    /// </summary>
    public interface IStatisticsRetailCountDAL
    {
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
    }
}
