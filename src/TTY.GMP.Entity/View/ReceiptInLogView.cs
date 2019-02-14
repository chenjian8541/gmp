using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.View
{
    /// <summary>
    /// 采购记录
    /// </summary>
    public class ReceiptInLogView
    {
        /// <summary>
        /// 单据编号
        /// </summary>
        public string InCode { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string GoodsSpec { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        public string GoodsCategoryName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public long InDate { get; set; }

        /// <summary>
        /// 时间描述
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
