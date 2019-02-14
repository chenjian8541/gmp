using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Config;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.Entity.CacheBucket
{
    /// <summary>
    /// 缓存所有的省、市、区信息
    /// </summary>
    public class AreaBucket : ICacheDataContract
    {
        /// <summary>
        /// 省、市、区信息
        /// </summary>
        public List<Area> Areas { get; set; }

        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public string GetKeyFormat(params object[] parms)
        {
            return "AreaBucket";
        }
    }
}
