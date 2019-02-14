using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.View
{
    /// <summary>
    /// 零售订单数统计
    /// </summary>
    public class DbStatisticsRetailBillCount
    {
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
        /// 销售数量
        /// </summary>
        public int BillCount { get; set; }
    }
}
