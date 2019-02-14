using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.User.Request
{
    /// <summary>
    /// 获取角色信息
    /// </summary>
    public class GetUserRoleRequest : RequestBase
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long UserRoleId { get; set; }
    }
}
