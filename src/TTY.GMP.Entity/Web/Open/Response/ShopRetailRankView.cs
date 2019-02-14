using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Web.Open.Response
{
    /// <summary>
    /// 门店销售单排名
    /// </summary>
    public class ShopRetailRankView
    {
        /// <summary>
        /// 排名
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 店长姓名
        /// </summary>
        public string ShopLinkMan { get; set; }

        /// <summary>
        /// 门店帐号
        /// </summary>
        public string ShopTelphone { get; set; }

        /// <summary>
        /// 订单数
        /// </summary>
        public int BillCount { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string DistrictName { get; set; }
    }
}
