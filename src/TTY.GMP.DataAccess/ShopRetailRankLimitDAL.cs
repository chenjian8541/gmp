using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.Database;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 门店销售单获奖者数据访问
    /// </summary>
    public class ShopRetailRankLimitDAL : IShopRetailRankLimitDAL, IDbContext<GmpDbContext>
    {
        /// <summary>
        /// 获取数据访问上下文
        /// </summary>
        /// <returns></returns>
        public GmpDbContext GetDbContext()
        {
            return new GmpDbContext();
        }

        /// <summary>
        /// 新增获奖者
        /// </summary>
        /// <param name="shopRetailRankWinners"></param>
        /// <returns></returns>
        public async Task AddShopRetailRankWinner(List<ShopRetailRankLimit> shopRetailRankWinners)
        {
            await this.InsertRange(shopRetailRankWinners);
        }

        /// <summary>
        /// 获取所有获奖者
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopRetailRankLimit>> GetShopRetailRankWinner()
        {
            return await this.FindList<GmpDbContext, ShopRetailRankLimit>();
        }

        /// <summary>
        /// 通过门店ID获取获奖者
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<ShopRetailRankLimit> GetShopRetailRankWinner(long shopId)
        {
            return await this.Find<GmpDbContext, ShopRetailRankLimit>(p => p.ShopId == shopId);
        }
    }
}
