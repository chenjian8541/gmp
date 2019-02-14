using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 零售退货单明细
    /// </summary>
    [Table("so_retail_back_detail")]
    public class SoRetailBackDetail
    {
        /// <summary>
        /// 退货单明细ID
        /// </summary>
        [Key]
        public long back_detail_id { get; set; }

        /// <summary>
        /// 退货单ID
        /// </summary>
        public long retail_back_id { get; set; }

        /// <summary>
        /// 退货明细ID
        /// </summary>
        public long retail_detail_id { get; set; }

        /// <summary>
        /// 入库仓库ID
        /// </summary>
        public long stock_id { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public long goods_id { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public long unit_id { get; set; }

        /// <summary>
        /// 退货数量
        /// </summary>
        public decimal back_qty { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal cost_price { get; set; }

        /// <summary>
        /// 原价金额（成本）
        /// </summary>
        public decimal cost_amount { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public decimal discount_rate { get; set; }

        /// <summary>
        /// 折后价
        /// </summary>
        public decimal discount_price { get; set; }

        /// <summary>
        /// 折扣额
        /// </summary>
        public decimal discount_amount { get; set; }

        /// <summary>
        /// 退货价
        /// </summary>
        public decimal back_price { get; set; }

        /// <summary>
        /// 退货金额
        /// </summary>
        public decimal back_amount { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>
        public long org_id { get; set; }

        /// <summary>
        /// 参考成本
        /// </summary>
        public decimal back_reference_cost { get; set; }

        /// <summary>
        /// 原始积分
        /// </summary>
        public decimal retail_original_integral { get; set; }

        /// <summary>
        /// 扣减积分
        /// </summary>
        public decimal retail_give_integral { get; set; }
    }
}
