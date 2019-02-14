using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.View;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 门店统计业务访问
    /// </summary>
    public class ShopStatisticsBLL : IShopStatisticsBLL
    {
        /// <summary>
        /// 门店统计数据访问
        /// </summary>
        private readonly IShopStatisticsDAL _shopStatisticsDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopStatisticsDal"></param>
        public ShopStatisticsBLL(IShopStatisticsDAL shopStatisticsDal)
        {
            this._shopStatisticsDal = shopStatisticsDal;
        }

        /// <summary>
        /// 获取全国门店统计信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<ShopStatisticsView>> GetShopStatistics(ShopStatisticsTypeEnum type)
        {
            return await _shopStatisticsDal.GetShopStatistics(type);
        }

        /// <summary>
        /// 更新全国门店统计信息
        /// </summary>
        /// <param name="type"></param>
        public async Task UpdateShopStatistics(ShopStatisticsTypeEnum type)
        {
            await _shopStatisticsDal.UpdateShopStatistics(type);
        }
    }
}
