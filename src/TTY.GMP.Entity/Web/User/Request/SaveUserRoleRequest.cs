using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.User.Request
{
    /// <summary>
    /// 保存用户角色信息
    /// </summary>
    public class SaveUserRoleRequest : RequestBase
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long UserRoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 选择的菜单
        /// </summary>
        public int[] Ids { get; set; }

        /// <summary>
        /// 数据较验
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }
            if (Ids == null || Ids.Length == 0)
            {
                return false;
            }
            return true;
        }
    }
}
