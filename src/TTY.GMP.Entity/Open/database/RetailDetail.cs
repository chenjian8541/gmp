using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Open.database
{
    /// <summary>
    /// 商品销售信息
    /// </summary>
    public class RetailDetail
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public long RetailId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string BillCode { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public long BillDate { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public long UnitId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public long GoodsId { get; set; }

        /// <summary>
        /// 商品类别ID
        /// </summary>
        public long GoodsCategoryId { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string GoodsSpec { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public long BrandId { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public long Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public long City { get; set; }

        /// <summary>
        /// 县、区
        /// </summary>
        public long District { get; set; }

        /// <summary>
        /// 地区、街道
        /// </summary>
        public long Street { get; set; }
    }
}
