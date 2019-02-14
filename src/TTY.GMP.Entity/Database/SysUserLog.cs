using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.Database
{
    /// <summary>
    /// 用户操作日志
    /// </summary>
    [Table("sysuserlog")]
    public class SysUserLog
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public long LogId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 操作类型 <see cref="TTY.GMP.Entity.Enum.UserLogEnum"/>
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime Ot { get; set; }
    }
}
