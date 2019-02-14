using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.DataAccess.Common
{
    /// <summary>
    /// 对缓存操作的一些配置
    /// 如：缓存过期时间
    /// </summary>
    public class CacheConfig
    {
        /// <summary>
        /// 默认一小时
        /// </summary>
        public static readonly TimeSpan TimeOutDefault = TimeSpan.FromHours(1);

        /// <summary>
        /// 一天
        /// </summary>
        public static readonly TimeSpan TimeOutOneDay = TimeSpan.FromDays(1);

        /// <summary>
        /// 一周
        /// </summary>
        public static readonly TimeSpan TimeOutWeeks = TimeSpan.FromDays(7);

        /// <summary>
        /// 登录失败时间，保存三十分钟
        /// </summary>
        public static readonly TimeSpan TimeOutLoginFailed = TimeSpan.FromMinutes(30);
    }
}
