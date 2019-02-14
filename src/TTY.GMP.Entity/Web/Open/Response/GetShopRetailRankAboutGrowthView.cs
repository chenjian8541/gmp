using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Web.Open.Response
{
    /// <summary>
    /// 获取门店销售单排名返回信息
    /// </summary>
    public class GetShopRetailRankAboutGrowthView
    {
        /// <summary>
        /// 第几周
        /// </summary>
        public int Week { get; set; }

        /// <summary>
        /// 类型  <see cref="TTY.GMP.Entity.Enum.GetShopRetailRankAboutGrowthTypeEnum"/>
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 订单数
        /// </summary>
        public int BillCount { get; set; }

        /// <summary>
        /// 需要订单数
        /// </summary>
        public int NeedBillCount { get; set; }

        /// <summary>
        /// 名次
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 门店销售单排名
        /// </summary>
        public List<ShopRetailRankView> ShopRetailRankViews { get; set; }
    }
}
