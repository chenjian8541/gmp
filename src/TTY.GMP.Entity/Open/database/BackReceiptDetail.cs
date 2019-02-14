using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Open.database
{
    /// <summary>
    /// 采购退货详情
    /// </summary>
    public class BackReceiptDetail
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public long GoodsId { get; set; }

        /// <summary>
        /// 采购数量
        /// </summary>
        public decimal Qty { get; set; }

        /// <summary>
        /// 采购单号
        /// </summary>
        public string BackCode { get; set; }

        /// <summary>
        /// 采购日期
        /// </summary>
        public long BackDate { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 采购门店名称
        /// </summary>
        public string ShopName { get; set; }

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

        /// <summary>
        /// 供应商ID
        /// </summary>
        public long BackOfferId { get; set; }

        /// <summary>
        /// 采购单位ID
        /// </summary>
        public long UnitId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public long GoodsCategoryId { get; set; }

        /// <summary>
        /// 品牌ID
        /// </summary>
        public long BrandId { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        public string GoodsSpec { get; set; }
    }
}
