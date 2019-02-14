using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.View
{
    /// <summary>
    /// 商品销售记录
    /// </summary>
    public class SaleGoodsLogView
    {
        /// <summary>
        /// 门店
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string GoodsSpec { get; set; }

        /// <summary>
        /// 登记证
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// 销售数量
        /// </summary>
        public decimal Qty { get; set; }

        /// <summary>
        /// 购买人名称
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 购买人电话
        /// </summary>
        public string MemberTel { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public long BillDate { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string BillCode { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string Identification { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 商品类别名称
        /// </summary>
        public string GoodsCategoryName { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }
    }
}
