using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Config;

namespace TTY.GMP.Entity.CacheBucket
{
    /// <summary>
    /// 用户登录失败记录
    /// </summary>
    public class UserLoginFailedBucket : ICacheDataContract
    {
        /// <summary>
        /// 登录帐号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 连续失败次数
        /// </summary>
        public int FailedCount { get; set; }

        /// <summary>
        /// 失败后禁止登录截至时间
        /// </summary>
        public DateTime ExpireAtTime { get; set; }

        /// <summary>
        /// 获取key
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public string GetKeyFormat(params object[] parms)
        {
            return string.Format("UserLoginFailedBucket_{0}", parms[0]);
        }
    }
}
