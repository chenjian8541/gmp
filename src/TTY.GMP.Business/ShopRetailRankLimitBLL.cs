using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 门店销售单获奖者业务访问
    /// </summary>
    public class ShopRetailRankLimitBLL : IShopRetailRankLimitBLL
    {
        /// <summary>
        /// 门店销售单获奖者数据访问
        /// </summary>
        private readonly IShopRetailRankLimitDAL _shopRetailRankWinnerDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopRetailRankWinnerDal"></param>
        public ShopRetailRankLimitBLL(IShopRetailRankLimitDAL shopRetailRankWinnerDal)
        {
            this._shopRetailRankWinnerDal = shopRetailRankWinnerDal;
        }

        /// <summary>
        /// 新增获奖者
        /// </summary>
        /// <param name="shopRetailRankWinners"></param>
        /// <returns></returns>
        public async Task AddShopRetailRankWinner(List<ShopRetailRankLimit> shopRetailRankWinners)
        {
            await this._shopRetailRankWinnerDal.AddShopRetailRankWinner(shopRetailRankWinners);
        }

        /// <summary>
        /// 获取所有获奖者
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopRetailRankLimit>> GetShopRetailRankWinner()
        {
            return await this._shopRetailRankWinnerDal.GetShopRetailRankWinner();
        }

        /// <summary>
        /// 通过门店ID获取获奖者
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<ShopRetailRankLimit> GetShopRetailRankWinner(long shopId)
        {
            return await this._shopRetailRankWinnerDal.GetShopRetailRankWinner(shopId);
        }
    }
}
