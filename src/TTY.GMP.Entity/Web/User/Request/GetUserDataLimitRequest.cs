using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.User.Request
{
    /// <summary>
    /// 获取用户数据权限信息
    /// </summary>
    public class GetUserDataLimitRequest : RequestBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 数据较验
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            return UserId > 0;
        }
    }
}
