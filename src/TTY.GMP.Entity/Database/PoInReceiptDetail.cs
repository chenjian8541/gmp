using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 采购入库单明细
    /// </summary>
    [Table("po_in_receipt_detail")]
    public class PoInReceiptDetail
    {
        /// <summary>
        /// id
        /// </summary>
        [Key]
        public long in_detail_id { get; set; }

        /// <summary>
        /// 采购入库单ID
        /// </summary>
        public long? in_id { get; set; }

        /// <summary>
        /// 入库仓库ID
        /// </summary>
        public long? stock_id { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public long goods_id { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal? qty { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal? price { get; set; }

        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal? amount { get; set; }

        /// <summary>
        /// 商品单位
        /// </summary>
        public long? unit_id { get; set; }

        /// <summary>
        /// 退货金额
        /// </summary>
        public decimal? discount { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        public decimal? discount_price { get; set; }

        /// <summary>
        /// 折后价
        /// </summary>
        public decimal? discount_amount { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        public decimal? tax { get; set; }

        /// <summary>
        /// 含税单价
        /// </summary>
        public decimal? tax_price { get; set; }

        /// <summary>
        /// 税额
        /// </summary>
        public decimal? tax_amount { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public decimal? explain { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public decimal? remark { get; set; }
    }
}
