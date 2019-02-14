using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Open.Request
{
    /// <summary>
    /// 获取AccessToken
    /// </summary>
    public class GetAccessTokenRequest
    {
        /// <summary>
        /// 用户唯一凭证
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 用户唯一凭证密钥
        /// </summary>
        public string Secret { get; set; }
    }
}
