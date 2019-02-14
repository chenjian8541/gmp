using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Web.User.Request
{
    /// <summary>
    /// 用户登录请求
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// 用户帐号
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }
    }
}
