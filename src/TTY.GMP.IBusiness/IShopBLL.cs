using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.View;
using TTY.GMP.Entity.Web.Shop.Request;

namespace TTY.GMP.IBusiness
{
    /// <summary>
    /// 门店业务访问
    /// </summary>
    public interface IShopBLL
    {
        /// <summary>
        /// 查询门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<Shop>, int>> GetShopPage(GetShopPageRequest request);

        /// <summary>
        /// 通过门店id集合查询门店
        /// 各id之间以“,”隔开
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<Shop>> GetShopByIds(string strIds);

        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<Shop> GetShop(long shopId);

        /// <summary>
        /// 获取门店零售单排名信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<List<ShopRetailRankView>> GetShopRetailRank(long startTime, long endTime);
    }
}
