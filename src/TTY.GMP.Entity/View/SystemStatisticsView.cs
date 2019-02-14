using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Config;

namespace TTY.GMP.Entity.View
{
    /// <summary>
    /// 系统统计
    /// </summary>
    public class SystemStatisticsView
    {
        /// <summary>
        /// 农户数量
        /// </summary>
        public int FarmerCount { get; set; }

        /// <summary>
        /// 门店数量
        /// </summary>
        public int ShopCount { get; set; }

        /// <summary>
        /// 经销商数量 
        /// </summary>
        public int DealerCount { get; set; }

        /// <summary>
        /// 生产商数量
        /// </summary>
        public int ProducersCount { get; set; }

        /// <summary>
        /// 销售记录数量
        /// </summary>
        public int SalesRecordsCount { get; set; }

        /// <summary>
        /// 准入商品数量
        /// </summary>
        public int AdmittanceGoodsCount { get; set; }

        /// <summary>
        /// 禁入商品数量
        /// </summary>
        public int ProhibitGoodsCount { get; set; }
    }
}
