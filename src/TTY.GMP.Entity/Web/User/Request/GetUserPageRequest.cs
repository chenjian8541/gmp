using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.User.Request
{
    /// <summary>
    /// 分页获取用户信息
    /// </summary>
    public class GetUserPageRequest : RequestPagingBase
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户帐号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 数据较验
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            return base.Validate();
        }
    }
}
