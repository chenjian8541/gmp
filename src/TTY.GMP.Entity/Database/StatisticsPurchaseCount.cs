using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 采购单据数统计
    /// </summary>
    [Table("statisticspurchasecount")]
    public class StatisticsPurchaseCount
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public long StatisticsId { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>
        public long OrgId { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 省ID
        /// </summary>
        public long Province { get; set; }

        /// <summary>
        /// 市ID 
        /// </summary>
        public long City { get; set; }

        /// <summary>
        ///  区
        /// </summary>
        public long District { get; set; }

        /// <summary>
        /// 街道、乡镇
        /// </summary>
        public long Street { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 销售数量
        /// </summary>
        public int BillCount { get; set; }
    }
}
