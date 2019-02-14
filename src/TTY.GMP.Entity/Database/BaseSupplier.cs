using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 供应商信息
    /// </summary>
    [Table("base_supplier")]
    public class BaseSupplier
    {
        /// <summary>
        /// 供应商ID
        /// </summary>
        [Key]
        public long supplier_id { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public long org_id { get; set; }

        /// <summary>
        /// 供应商类别ID
        /// </summary>
        public long supplier_cat_id { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        public string supplier_code { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string supplier_name { get; set; }

        /// <summary>
        /// 供应商简称
        /// </summary>
        public string supplier_short_name { get; set; }

        /// <summary>
        /// 供应商电话
        /// </summary>
        public string supplier_tel { get; set; }

        /// <summary>
        /// 供应商传真
        /// </summary>
        public string supplier_fax { get; set; }

        /// <summary>
        /// 供应商地址
        /// </summary>
        public string supplier_address { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string pic { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public string supplier_bank { get; set; }

        /// <summary>
        /// 银行账户
        /// </summary>
        public long? supplier_account { get; set; }

        /// <summary>
        /// 银行户名
        /// </summary>
        public string supplier_bank_holder { get; set; }

        /// <summary>
        /// 纳税人识别码
        /// </summary>
        public string supplier_taxpayer_code { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string comment { get; set; }

        /// <summary>
        /// 是否逻辑删除：1:表示已经逻辑删除，0：表示逻辑删除
        /// </summary>
        public byte? is_deleted { get; set; }

        /// <summary>
        /// 状态：  1. 启用  0：停用
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long? creater { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long? created_time { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public long? modifier { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public long? modified_time { get; set; }
    }
}
