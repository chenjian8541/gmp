using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 门店销售单获奖者数据访问
    /// </summary>
    public interface IShopRetailRankLimitDAL
    {
        /// <summary>
        /// 新增获奖者
        /// </summary>
        /// <param name="shopRetailRankWinners"></param>
        /// <returns></returns>
        Task AddShopRetailRankWinner(List<ShopRetailRankLimit> shopRetailRankWinners);

        /// <summary>
        /// 获取所有获奖者
        /// </summary>
        /// <returns></returns>
        Task<List<ShopRetailRankLimit>> GetShopRetailRankWinner();

        /// <summary>
        /// 通过门店ID获取获奖者
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<ShopRetailRankLimit> GetShopRetailRankWinner(long shopId);
    }
}
