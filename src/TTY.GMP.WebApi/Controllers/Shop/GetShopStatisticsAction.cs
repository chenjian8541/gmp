using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.View;
using TTY.GMP.Entity.Web.Shop.Request;
using TTY.GMP.Entity.Web.Shop.Response;
using TTY.GMP.IBusiness;

namespace TTY.GMP.WebApi.Controllers.Shop
{
    /// <summary>
    /// 获取门店统计信息
    /// </summary>
    public class GetShopStatisticsAction
    {
        /// <summary>
        /// 门店统计业务
        /// </summary>
        private readonly IShopStatisticsBLL _shopStatisticsBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopStatisticsBll"></param>
        public GetShopStatisticsAction(IShopStatisticsBLL shopStatisticsBll)
        {
            this._shopStatisticsBll = shopStatisticsBll;
        }

        /// <summary>
        /// 获取门店统计信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(GetShopStatisticsRequest request)
        {
            List<ShopStatisticsView> shopStatistics = null;
            if (request.Province > 0)
            {
                shopStatistics = await _shopStatisticsBll.GetShopStatistics(ShopStatisticsTypeEnum.City);
                if (shopStatistics != null)
                {
                    shopStatistics = shopStatistics.Where(p => p.Province == request.Province).ToList();
                }
            }
            else
            {
                shopStatistics = await _shopStatisticsBll.GetShopStatistics(ShopStatisticsTypeEnum.Province);
            }
            if (shopStatistics == null)
            {
                shopStatistics = new List<ShopStatisticsView>();
            }
            var statisticsView = new GetShopStatisticsView(shopStatistics, shopStatistics.Sum(p => p.ShopCount));
            return ResponseBase.Success(statisticsView);
        }
    }
}
