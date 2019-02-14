using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Web.Report.Response
{
    /// <summary>
    /// 区域农药库存统计
    /// </summary>
    public class StatisticsStockView
    {
        /// <summary>
        /// 区域农药库存
        /// </summary>
        public List<AreaStockView> AreaStocks { get; set; }

        /// <summary>
        /// 合计信息
        /// </summary>
        public AreaStockSumView AreaStockSum { get; set; }
    }

    /// <summary>
    /// 区域农药库存
    /// </summary>
    public class AreaStockView : StatisticsGoodsView
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
    }

    /// <summary>
    /// 区域农药库存合计
    /// </summary>
    public class AreaStockSumView : StatisticsGoodsView
    {
    }
}
