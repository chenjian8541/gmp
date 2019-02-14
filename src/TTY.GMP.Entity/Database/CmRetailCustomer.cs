using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 零售客户
    /// </summary>
    [Table("cm_retail_customer")]
    public class CmRetailCustomer
    {
        /// <summary>
        /// 零售客户ID
        /// </summary>
        [Key]
        [Column("retail_customer_id")]
        public long RetailCustomerId { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        [Column("org_id")]
        public long OrgId { get; set; }

        /// <summary>
        /// 所属门店id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Column("retail_customer_name")]
        public string RetailCustomerName { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [Column("retail_customer_tel")]
        public string RetailCustomerTel { get; set; }

        /// <summary>
        /// 客户简称
        /// </summary>
        [Column("retail_customer_short_name")]
        public string RetailCustomerShortName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [Column("identification")]
        public string Identification { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [Column("province")]
        public long? Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [Column("city")]
        public long? City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        [Column("district")]
        public long? District { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        [Column("street")]
        public long? Street { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [Column("address_detail")]
        public string AddressDetail { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Column("birthday")]
        public int? Birthday { get; set; }

        /// <summary>
        /// 性别（0：男；1：女）
        /// </summary>
        [Column("retail_customer_gender")]
        public int RetailCustomerGender { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [Column("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// 是否删除 1是 0否
        /// </summary>
        [Column("is_deleted")]
        public byte IsDeleted { get; set; }

        /// <summary>
        /// 状态（0：启用；1：停用）
        /// </summary>
        [Column("status")]
        public int Status { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("creater")]
        public long Creater { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("created_time")]
        public long CreatedTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [Column("modifier")]
        public long? Modifier { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("modified_time")]
        public long? ModifiedTime { get; set; }
    }
}
