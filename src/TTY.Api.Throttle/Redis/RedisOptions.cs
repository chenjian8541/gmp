using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.Api.Throttle.Redis
{
    public class RedisOptions
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { set; get; }

        /// <summary>
        /// 默认库
        /// </summary>
        public int DefaultDb { get; set; }

        /// <summary>
        /// Key前缀
        /// </summary>
        public string KeyPrefix { set; get; } = "apithrottle";
    }
}
