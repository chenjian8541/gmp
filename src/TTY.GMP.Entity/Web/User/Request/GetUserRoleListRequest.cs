using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.User.Request
{
    /// <summary>
    /// 获取角色列表
    /// </summary>
    public class GetUserRoleListRequest : RequestBase
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
    }
}
