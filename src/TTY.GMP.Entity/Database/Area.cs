using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 地区
    /// </summary>
    [Table("base_area")]
    public class Area
    {
        /// <summary>
        /// 区域主键
        /// </summary>		
        [Key]
        [Column("area_id")]
        public long AreaId { get; set; }

        /// <summary>
        /// 父级主键
        /// </summary>	
        [Column("parent_id")]
        public long ParentId { get; set; }

        /// <summary>
        /// 区域编码+
        /// </summary>	
        [Column("area_code")]
        public string AreaCode { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>	
        [Column("area_name")]
        public string AreaName { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [Column("longitude")]
        public string Longitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [Column("latitude")]
        public string Latitude { get; set; }

        /// <summary>
        /// 等级
        /// <see cref="TTY.GMP.Entity.Enum.AreaLevelEnum"/>
        /// </summary>
        [Column("level")]
        public string Level { get; set; }

        /// <summary>
        /// 状态 1正常 2停用
        /// </summary>
        [Column("status")]
        public int Status { get; set; }

        /// <summary>
        /// 是否删除 1已经删除 0正常
        /// </summary>
        [Column("is_deleted")]
        public int IsDeleted { get; set; }
    }
}
