using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 零售订单详情
    /// </summary>
    [Table("so_retail_detail")]
    public class SoRetailDetail
    {
        /// <summary>
        /// 零售单明细ID
        /// </summary>
        [Key]
        [Column("retail_detail_id")]
        public long RetailDetailId { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>
        [Column("org_id")]
        public long? OrgId { get; set; }

        /// <summary>
        /// 零售单单头ID
        /// </summary>
        [Column("bill_id")]
        public long BillId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [Column("goods_id")]
        public long GoodsId { get; set; }

        /// <summary>
        /// 零售单数量
        /// </summary>
        [Column("qty")]
        public decimal Qty { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Column("unit_id")]
        public long UnitId { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        [Column("cost_price")]
        public decimal CostPrice { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        [Column("discount_rate")]
        public decimal DiscountRate { get; set; }

        /// <summary>
        /// 折扣后单价
        /// </summary>
        [Column("discount_price")]
        public decimal DiscountPrice { get; set; }

        /// <summary>
        /// 销售价（实际成交价）
        /// </summary>
        [Column("sale_price")]
        public decimal SalePrice { get; set; }

        /// <summary>
        /// 原价总额
        /// </summary>
        [Column("cost_amount")]
        public decimal CostAmount { get; set; }

        /// <summary>
        /// 销售金额(实际成交金额)
        /// </summary>
        [Column("sale_amount")]
        public decimal SaleAmount { get; set; }

        /// <summary>
        /// 折扣金额
        /// </summary>
        [Column("discount_amount")]
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 参考成本
        /// </summary>
        [Column("reference_cost")]
        public decimal ReferenceCost { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        [Column("tax")]
        public decimal Tax { get; set; }

        /// <summary>
        /// 含税单价
        /// </summary>
        [Column("tax_price")]
        public decimal TaxPrice { get; set; }

        /// <summary>
        /// 含税金额
        /// </summary>
        [Column("tax_amount")]
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// 原始积分
        /// </summary>
        [Column("original_integral")]
        public decimal OriginalIntegral { get; set; }

        /// <summary>
        /// 赠送积分
        /// </summary>
        [Column("give_integral")]
        public decimal GiveIntegral { get; set; }

        /// <summary>
        /// 是否赠送（1：是；0：否）
        /// </summary>
        [Column("is_gift")]
        public int IsGift { get; set; }

        /// <summary>
        /// 已退数量
        /// </summary>
        [Column("returned_qty")]
        public decimal ReturnedQty { get; set; }
    }
}
