using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Web.Report.Response
{
    /// <summary>
    /// 区域门店库存看板
    /// </summary>
    public class StatisticsStockShopView
    {
        /// <summary>
        /// 区域门店库存看板
        /// </summary>
        public List<AreaStockShopView> AreaStockShops { get; set; }
    }

    /// <summary>
    /// 区域门店库存看板
    /// </summary>
    public class AreaStockShopView : StatisticsGoodsView
    {
        /// <summary>
        /// 地区ID
        /// </summary>
        public long AreaId { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 地区级别
        /// </summary>
        public string AreaLevel { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
    }
}
