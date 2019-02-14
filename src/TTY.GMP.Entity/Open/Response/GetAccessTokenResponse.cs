using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Open.Response
{
    /// <summary>
    /// AccessToken
    /// </summary>
    public class GetAccessTokenResponse
    {
        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int ExpiresIn { get; set; }
    }
}
