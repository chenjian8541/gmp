using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.View;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 门店统计数据访问
    /// </summary>
    public interface IShopStatisticsDAL
    {
        /// <summary>
        /// 获取全国门店统计信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<List<ShopStatisticsView>> GetShopStatistics(ShopStatisticsTypeEnum type);

        /// <summary>
        /// 更新全国门店统计信息
        /// </summary>
        /// <param name="type"></param>
        Task UpdateShopStatistics(ShopStatisticsTypeEnum type);
    }
}
