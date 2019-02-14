using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.View
{
    /// <summary>
    /// 采购入库单明细
    /// </summary>
    public class PoInReceiptDetailView
    {
        /// <summary>
        /// id
        /// </summary>
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

        /// <summary>
        /// 毒性
        /// </summary>
        public string toxicity_grade_name { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string goods_name { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string goods_code { get; set; }

        /// <summary>
        /// 商品剂型
        /// </summary>
        public string dosage_forms { get; set; }

        /// <summary>
        /// 登记证号
        /// </summary>
        public string registration_number { get; set; }

        /// <summary>
        /// 登记证持有人
        /// </summary>
        public string registration_holder { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string goods_product { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        public string goods_spec { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string unit_name { get; set; }

        /// <summary>
        /// 是否限制性农药
        /// </summary>
        public int goods_restrictive { get; set; }
    }
}
