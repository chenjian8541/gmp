using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.User.Request
{
    /// <summary>
    /// 改变用户状态
    /// </summary>
    public class ChangeStatusFlagRequest : RequestBase
    {
        /// <summary>
        /// 用户状态
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 需要设置成的状态
        /// <see cref="TTY.GMP.Entity.Enum.UserStatusFlagEnum"/>
        /// </summary>
        public int NewStatusFlag { get; set; }

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
            return true;
        }
    }
}
