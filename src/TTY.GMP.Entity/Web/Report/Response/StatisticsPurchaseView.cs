using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Web.Report.Response
{
    /// <summary>
    /// 区域农药采购统计
    /// </summary>
    public class StatisticsPurchaseView
    {
        /// <summary>
        /// 区域采购信息
        /// </summary>
        public List<AreaPurchaseView> AreaPurchases { get; set; }

        /// <summary>
        /// 合计信息
        /// </summary>
        public AreaPurchaseSumView AreaPurchaseSum { get; set; }

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
    /// 区域采购
    /// </summary>
    public class AreaPurchaseView : StatisticsGoodsView
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
        /// 销售单数
        /// </summary>
        public decimal Count { get; set; }
    }

    /// <summary>
    /// 区域采购统计
    /// </summary>
    public class AreaPurchaseSumView : StatisticsGoodsView
    {
        /// <summary>
        /// 采购单数
        /// </summary>
        public decimal Count { get; set; }
    }
}
