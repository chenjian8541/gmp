using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.View
{
    /// <summary>
    /// 商品
    /// </summary>
    public class GoodsView
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public long goods_id { get; set; }

        /// <summary>
        /// 商品类别ID
        /// </summary>
        public long goods_class_id { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public long org_id { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string goods_code { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string goods_name { get; set; }

        /// <summary>
        /// 商品简称
        /// </summary>
        public string goods_short_name { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        public string goods_spec { get; set; }

        /// <summary>
        /// 商品拼音码
        /// </summary>
        public string goods_pycode { get; set; }

        /// <summary>
        /// 商品产地
        /// </summary>
        public string goods_origin { get; set; }

        /// <summary>
        /// 商品成分
        /// </summary>
        public string goods_ingredient { get; set; }

        /// <summary>
        /// 剂型
        /// </summary>
        public string dosage_forms { get; set; }

        /// <summary>
        /// 含量
        /// </summary>
        public string contents { get; set; }

        /// <summary>
        /// 是否服务商品
        /// </summary>
        public byte? goods_is_service { get; set; }

        /// <summary>
        /// 是否删除 1是 0否
        /// </summary>
        public byte? is_deleted { get; set; }

        /// <summary>
        /// 状态（0：启用；1：停用）
        /// </summary>
        public int? status { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string comment { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? show_index { get; set; }

        /// <summary>
        /// 基本商品单位ID
        /// </summary>
        public long base_unit_id { get; set; }

        /// <summary>
        /// 采购默认单位ID
        /// </summary>
        public long purchase_unit_id_defult { get; set; }

        /// <summary>
        /// 销售默认单位ID
        /// </summary>
        public long sale_unit_id_defult { get; set; }

        /// <summary>
        /// 库存默认单位ID
        /// </summary>
        public long store_unit_id_defult { get; set; }

        /// <summary>
        /// 采购默认税率
        /// </summary>
        public long purchase_tax_rate_defult { get; set; }

        /// <summary>
        /// 销售默认税率
        /// </summary>
        public long sale_tax_rate_defult { get; set; }

        /// <summary>
        /// 商品品牌ID
        /// </summary>
        public long brand_id { get; set; }

        /// <summary>
        /// 税率ID
        /// </summary>
        public long rate_id { get; set; }

        /// <summary>
        /// 商品单位ID
        /// </summary>
        public long unit_id { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        public string barcode { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        public string qrcode { get; set; }

        /// <summary>
        /// 登记证号
        /// </summary>
        public string registration_number { get; set; }

        /// <summary>
        /// 登记证持有人
        /// </summary>
        public string registration_holder { get; set; }

        /// <summary>
        /// 毒性级别ID
        /// </summary>
        public long? toxicity_grade_id { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string goods_product { get; set; }

        /// <summary>
        /// 是否是限制性农药
        /// </summary>
        public int goods_restrictive { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long creater { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long created_time { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public long modifier { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public long modified_time { get; set; }

        /// <summary>
        /// 毒性
        /// </summary>
        public string toxicity_grade_name { get; set; }
    }
}
