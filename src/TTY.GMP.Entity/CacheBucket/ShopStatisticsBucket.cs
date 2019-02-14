using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Config;
using TTY.GMP.Entity.View;

namespace TTY.GMP.Entity.CacheBucket
{
    /// <summary>
    /// 缓存门店统计信息
    /// </summary>
    public class ShopStatisticsBucket : ICacheDataContract
    {
        /// <summary>
        /// 门店统计信息
        /// </summary>
        public List<ShopStatisticsView> ShopStatistics { get; set; }

        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public string GetKeyFormat(params object[] parms)
        {
            return string.Format("ShopStatisticsBucket_{0}", parms[0]);
        }
    }
}
