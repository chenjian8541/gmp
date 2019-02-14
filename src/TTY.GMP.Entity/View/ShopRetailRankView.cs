using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.View
{
    /// <summary>
    /// 门店零售单数量
    /// </summary>
    public class ShopRetailRankView
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 门店联系人
        /// </summary>
        public string ShopLinkMan { get; set; }

        /// <summary>
        /// 门店联系电话
        /// </summary>
        public string ShopTelphone { get; set; }

        /// <summary>
        /// 门店联系地址
        /// </summary>
        public string ShopAddress { get; set; }

        /// <summary>
        /// 零售单数量
        /// </summary>
        public int BillCount { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public long OrgId { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

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
