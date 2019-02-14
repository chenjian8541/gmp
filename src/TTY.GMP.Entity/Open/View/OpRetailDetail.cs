using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Open.View
{
    /// <summary>
    /// 销售详情
    /// </summary>
    public class OpRetailDetail
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string BillCode { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime BillDate { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 购买者名称
        /// </summary>
        public string BuyerName { get; set; }

        /// <summary>
        /// 购买者号码
        /// </summary>
        public string BuyerTel { get; set; }

        /// <summary>
        /// 购买者身份证号码
        /// </summary>
        public string BuyerIdentification { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string GoodsCategoryName { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string GoodsBrand { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string GoodsSpec { get; set; }
        
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 数量
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
        /// 县、区
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 地区、街道
        /// </summary>
        public string Street { get; set; }
    }
}
