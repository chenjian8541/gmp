using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 商品表
    /// </summary>
    [Table("sys_goods")]
    public class Goods
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Key]
        [Column("goods_id")]
        public long goods_id { get; set; }

        /// <summary>
        /// 商品类别ID
        /// </summary>
        [Column("goods_class_id")]
        public long goods_class_id { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        [Column("org_id")]
        public long org_id { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        [Column("goods_code")]
        public string goods_code { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("goods_name")]
        public string goods_name { get; set; }

        /// <summary>
        /// 商品简称
        /// </summary>
        [Column("goods_short_name")]
        public string goods_short_name { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        [Column("goods_spec")]
        public string goods_spec { get; set; }

        /// <summary>
        /// 商品拼音码
        /// </summary>
        [Column("goods_pycode")]
        public string goods_pycode { get; set; }

        /// <summary>
        /// 商品产地
        /// </summary>
        [Column("goods_origin")]
        public string goods_origin { get; set; }

        /// <summary>
        /// 商品成分
        /// </summary>
        [Column("goods_ingredient")]
        public string goods_ingredient { get; set; }

        /// <summary>
        /// 剂型
        /// </summary>
        [Column("dosage_forms")]
        public string dosage_forms { get; set; }

        /// <summary>
        /// 含量
        /// </summary>
        [Column("contents")]
        public string contents { get; set; }

        /// <summary>
        /// 是否服务商品
        /// </summary>
        [Column("goods_is_service")]
        public byte? goods_is_service { get; set; }

        /// <summary>
        /// 是否删除 1是 0否
        /// </summary>
        [Column("is_deleted")]
        public byte? is_deleted { get; set; }

        /// <summary>
        /// 状态（0：启用；1：停用）
        /// </summary>
        [Column("status")]
        public int? status { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [Column("comment")]
        public string comment { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Column("show_index")]
        public int? show_index { get; set; }

        /// <summary>
        /// 基本商品单位ID
        /// </summary>
        [Column("base_unit_id")]
        public long base_unit_id { get; set; }

        /// <summary>
        /// 采购默认单位ID
        /// </summary>
        [Column("purchase_unit_id_defult")]
        public long purchase_unit_id_defult { get; set; }

        /// <summary>
        /// 销售默认单位ID
        /// </summary>
        [Column("sale_unit_id_defult")]
        public long sale_unit_id_defult { get; set; }

        /// <summary>
        /// 库存默认单位ID
        /// </summary>
        [Column("store_unit_id_defult")]
        public long store_unit_id_defult { get; set; }

        /// <summary>
        /// 采购默认税率
        /// </summary>
        [Column("purchase_tax_rate_defult")]
        public long purchase_tax_rate_defult { get; set; }

        /// <summary>
        /// 销售默认税率
        /// </summary>
        [Column("sale_tax_rate_defult")]
        public long sale_tax_rate_defult { get; set; }

        /// <summary>
        /// 商品品牌ID
        /// </summary>
        [Column("brand_id")]
        public long brand_id { get; set; }

        /// <summary>
        /// 税率ID
        /// </summary>
        [Column("rate_id")]
        public long rate_id { get; set; }

        /// <summary>
        /// 商品单位ID
        /// </summary>
        [Column("unit_id")]
        public long unit_id { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        [Column("barcode")]
        public string barcode { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        [Column("qrcode")]
        public string qrcode { get; set; }

        /// <summary>
        /// 登记证号
        /// </summary>
        [Column("registration_number")]
        public string registration_number { get; set; }

        /// <summary>
        /// 登记证持有人
        /// </summary>
        [Column("registration_holder")]
        public string registration_holder { get; set; }

        /// <summary>
        /// 毒性级别ID
        /// </summary>
        [Column("toxicity_grade_id")]
        public long? toxicity_grade_id { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        [Column("goods_product")]
        public string goods_product { get; set; }

        /// <summary>
        /// 是否是限制性农药
        /// </summary>
        [Column("goods_restrictive")]
        public int goods_restrictive { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("creater")]
        public long creater { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("created_time")]
        public long created_time { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [Column("modifier")]
        public long modifier { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("modified_time")]
        public long modified_time { get; set; }
    }
}
