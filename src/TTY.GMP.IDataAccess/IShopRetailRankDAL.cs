using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 门店销售单排名数据访问
    /// </summary>
    public interface IShopRetailRankDAL
    {
        /// <summary>
        /// 批量新增门店销售单排名数据
        /// </summary>
        /// <param name="shopRetailRanks"></param>
        /// <returns></returns>
        Task AddShopRetailRank(List<ShopRetailRank> shopRetailRanks);

        /// <summary>
        /// 删除此时间之前的排名数据
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        Task DelShopRetailRank(DateTime time);

        /// <summary>
        /// 标记之前的数据
        /// </summary>
        /// <returns></returns>
        Task MarkShopRetailRankOld();

        /// <summary>
        /// 获取上一次排名情况
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<IEnumerable<ShopRetailRank>> GetLastShopRetailRank(int type);
    }
}
