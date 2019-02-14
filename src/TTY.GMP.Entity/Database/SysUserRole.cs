using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Database
{
    [Table("sysuserrole")]
    public class SysUserRole
    {
        /// <summary>
        ///用户角色ID
        /// </summary>
        [Key]
        public long UserRoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        public string AuthorityValue { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
