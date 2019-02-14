using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.View
{
    /// <summary>
    /// 零售订单详情
    /// </summary>
    public class SoRetailDetailView
    {
        /// <summary>
        /// 零售单明细ID
        /// </summary>
        public long retail_detail_id { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>
        public long? org_id { get; set; }

        /// <summary>
        /// 零售单单头ID
        /// </summary>
        public long bill_id { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public long goods_id { get; set; }

        /// <summary>
        /// 零售单数量
        /// </summary>
        public decimal qty { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public long unit_id { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal cost_price { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public decimal discount_rate { get; set; }

        /// <summary>
        /// 折扣后单价
        /// </summary>
        public decimal discount_price { get; set; }

        /// <summary>
        /// 销售价（实际成交价）
        /// </summary>
        public decimal sale_price { get; set; }

        /// <summary>
        /// 原价总额
        /// </summary>
        public decimal cost_amount { get; set; }

        /// <summary>
        /// 销售金额(实际成交金额)
        /// </summary>
        public decimal sale_amount { get; set; }

        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal discount_amount { get; set; }

        /// <summary>
        /// 参考成本
        /// </summary>
        public decimal reference_cost { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        public decimal tax { get; set; }

        /// <summary>
        /// 含税单价
        /// </summary>
        public decimal tax_price { get; set; }

        /// <summary>
        /// 含税金额
        /// </summary>
        public decimal tax_amount { get; set; }

        /// <summary>
        /// 原始积分
        /// </summary>
        public decimal original_integral { get; set; }

        /// <summary>
        /// 赠送积分
        /// </summary>
        public decimal give_integral { get; set; }

        /// <summary>
        /// 是否赠送（1：是；0：否）
        /// </summary>
        public int is_gift { get; set; }

        /// <summary>
        /// 已退数量
        /// </summary>
        public decimal returned_qty { get; set; }

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
