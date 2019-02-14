using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 零售订单信息
    /// </summary>
    [Table("so_retail_info")]
    public class SoRetailInfo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("retail_info_id")]
        public long retail_info_id { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        [Column("retail_id")]
        public long retail_id { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        [Column("org_id")]
        public long? org_id { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        [Column("retail_customer_id")]
        public long? retail_customer_id { get; set; }
    }
}
