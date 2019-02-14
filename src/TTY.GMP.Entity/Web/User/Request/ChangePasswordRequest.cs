using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.User.Request
{
    /// <summary>
    /// 修改密码
    /// </summary>
    public class ChangePasswordRequest : RequestBase
    {
        /// <summary>
        /// 原始密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// 数据较验
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (string.IsNullOrEmpty(OldPassword))
            {
                return false;
            }
            if (string.IsNullOrEmpty(NewPassword))
            {
                return false;
            }
            return true;
        }
    }
}
