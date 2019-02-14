using System;
using System.Collections.Generic;

namespace TTY.GMP.Entity.Web.Report.Response
{
    /// <summary>
    /// 区域农药销售统计
    /// </summary>
    public class StatisticsRetailView
    {
        /// <summary>
        /// 区域零售信息
        /// </summary>
        public List<AreaRetailView> AreaRetails { get; set; }

        /// <summary>
        /// 合计信息
        /// </summary>
        public AreaRetailSumView AreaRetailSum { get; set; }

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
    /// 区域零售
    /// </summary>
    public class AreaRetailView : StatisticsGoodsView
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
    /// 区域零售统计
    /// </summary>
    public class AreaRetailSumView : StatisticsGoodsView
    {
        /// <summary>
        /// 销售单数
        /// </summary>
        public decimal Count { get; set; }
    }
}
