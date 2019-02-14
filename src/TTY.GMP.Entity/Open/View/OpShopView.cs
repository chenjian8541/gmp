using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Open.View
{
    /// <summary>
    /// 门店信息
    /// </summary>
    public class OpShopView
    {
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 门店联系人
        /// </summary>
        public string ShopLinkMan { get; set; }

        /// <summary>
        /// 门店电话
        /// </summary>
        public string ShopTelphone { get; set; }

        /// <summary>
        /// 门店地址
        /// </summary>
        public string ShopAddress { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区、县
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 街道、乡镇
        /// </summary>
        public string Stree { get; set; }
    }
}
