using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 采购退货单明细
    /// </summary>
    [Table("po_back_receipt_detail")]
    public class PoBackReceiptDetail
    {
        /// <summary>
        /// 采购退货单明细ID
        /// </summary>
        [Key]
        public long back_detail_id { get; set; }

        /// <summary>
        /// 出库仓库
        /// </summary>
        public long stock_id { get; set; }

        /// <summary>
        /// 退货单表头ID
        /// </summary>
        public long back_id { get; set; }

        /// <summary>
        /// 商品iD
        /// </summary>
        public long goods_id { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal? qty { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal? price { get; set; }

        /// <summary>
        /// 退货金额
        /// </summary>
        public decimal? amount { get; set; }

        /// <summary>
        /// 退货单位
        /// </summary>
        public long unit_id { get; set; }

        /// <summary>
        /// 退货折扣
        /// </summary>
        public decimal? discount { get; set; }

        /// <summary>
        /// 折扣单价
        /// </summary>
        public decimal? discount_price { get; set; }

        /// <summary>
        /// 折扣金额
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
        /// 含税金额
        /// </summary>
        public decimal? tax_amount { get; set; }
    }
}
