using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Web.Report.Response
{
    /// <summary>
    /// 区域门店采购看板
    /// </summary>
    public class StatisticsPurchaseShopView
    {
        /// <summary>
        /// 区域门店采购
        /// </summary>
        public List<AreaShopPurchaseView> AreaShopPurchases { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }

    /// <summary>
    /// 区域门店采购
    /// </summary>
    public class AreaShopPurchaseView : StatisticsGoodsView
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

        /// <summary>
        /// 销售单数
        /// </summary>
        public decimal Count { get; set; }
    }
}
