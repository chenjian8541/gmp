using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.User.Request
{
    /// <summary>
    /// 重置密码
    /// </summary>
    public class ResetPasswordRequest : RequestBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

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
            if (UserId <= 0)
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
