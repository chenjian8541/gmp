using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Web.Open.Response
{
    /// <summary>
    /// 门店销售单排名信息(订单王)
    /// </summary>
    public class GetShopRetailRankAboutKingView
    {
        /// <summary>
        /// 类型  <see cref="TTY.GMP.Entity.Enum.GetShopRetailRankAboutKingTypeEnum"/>
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
