using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;
using System.Linq;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 门店销售单排名业务
    /// </summary>
    public class ShopRetailRankBLL : IShopRetailRankBLL
    {
        /// <summary>
        /// 门店销售单数据访问
        /// </summary>
        private readonly IShopRetailRankDAL _shopRetailRankDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopRetailRankDal"></param>
        public ShopRetailRankBLL(IShopRetailRankDAL shopRetailRankDal)
        {
            this._shopRetailRankDal = shopRetailRankDal;
        }

        /// <summary>
        /// 批量新增门店销售单排名数据
        /// </summary>
        /// <param name="shopRetailRanks"></param>
        /// <returns></returns>
        public async Task AddShopRetailRank(List<ShopRetailRank> shopRetailRanks)
        {
            await this._shopRetailRankDal.AddShopRetailRank(shopRetailRanks);
        }

        /// <summary>
        /// 删除此时间之前的排名数据
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task DelShopRetailRank(DateTime time)
        {
            await this._shopRetailRankDal.DelShopRetailRank(time);
        }

        /// <summary>
        /// 标记之前的数据
        /// </summary>
        /// <returns></returns>
        public async Task MarkShopRetailRankOld()
        {
            await this._shopRetailRankDal.MarkShopRetailRankOld();
        }

        /// <summary>
        /// 获取上一次排名情况
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<ShopRetailRank>> GetLastShopRetailRank(int type)
        {
            var result = await this._shopRetailRankDal.GetLastShopRetailRank(type);
            return result.ToList();
        }
    }
}
