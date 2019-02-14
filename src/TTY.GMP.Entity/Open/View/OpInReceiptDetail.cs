using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Open.View
{
    /// <summary>
    /// 采购详情
    /// </summary>
    public class OpInReceiptDetail
    {
        /// <summary>
        /// 单号
        /// </summary>
        public string InCode { get; set; }

        /// <summary>
        /// 采购日期
        /// </summary>
        public DateTime InDate { get; set; }

        /// <summary>
        /// 采购门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品类别名称
        /// </summary>
        public string GoodsCategoryName { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string GoodsBrand { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        public string GoodsSpec { get; set; }

        /// <summary>
        ///  采购单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 采购数量
        /// </summary>
        public decimal Qty { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 地区、县
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 乡镇、街道
        /// </summary>
        public string Street { get; set; }
    }
}
