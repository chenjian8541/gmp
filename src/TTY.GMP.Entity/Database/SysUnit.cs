using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 商品单位
    /// </summary>
    [Table("sys_unit")]
    public class SysUnit
    {
        /// <summary>
        /// 商品单位ID
        /// </summary>
        /// <returns></returns>
        [Key]
        [Column("unit_id")]
        public long UnitId { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        /// <returns></returns>
        [Column("org_id")]
        public long OrgId { get; set; }
        /// <summary>
        /// 商品单位名称
        /// </summary>
        /// <returns></returns>
        [Column("unit_name")]
        public string UnitName { get; set; }
        /// <summary>
        /// 1:表示已经逻辑删除，0：表示为未删除。
        /// </summary>
        /// <returns></returns>
        [Column("is_deleted")]
        public int? IsDeleted { get; set; }
        /// <summary>
        /// 状态： 1启用  0停用
        /// </summary>
        /// <returns></returns>
        [Column("status")]
        public int? Status { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        /// <returns></returns>
        [Column("creater")]
        public long? Creater { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("created_time")]
        public long? CreatedTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        /// <returns></returns>
        [Column("modifier")]
        public long? Modifier { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [Column("modified_time")]
        public long? ModifiedTime { get; set; }
    }
}
