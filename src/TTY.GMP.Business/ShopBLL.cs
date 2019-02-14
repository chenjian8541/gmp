using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.View;
using TTY.GMP.Entity.Web.Shop.Request;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 门店业务访问
    /// </summary>
    public class ShopBLL : IShopBLL
    {
        /// <summary>
        /// 门店数据访问
        /// </summary>
        private readonly IShopDAL _shopDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopDal"></param>

        public ShopBLL(IShopDAL shopDal)
        {
            this._shopDal = shopDal;
        }

        /// <summary>
        /// 查询门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<Shop>, int>> GetShopPage(GetShopPageRequest request)
        {
            return await _shopDal.GetShopPage(request);
        }

        /// <summary>
        /// 通过门店id集合查询门店
        /// 各id之间以“,”隔开
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<Shop>> GetShopByIds(string strIds)
        {
            return await _shopDal.GetShopByIds(strIds);
        }

        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<Shop> GetShop(long shopId)
        {
            return await _shopDal.GetShop(shopId);
        }

        /// <summary>
        /// 获取门店零售单排名信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<ShopRetailRankView>> GetShopRetailRank(long startTime, long endTime)
        {
            return await _shopDal.GetShopRetailRank(startTime, endTime);
        }
    }
}
