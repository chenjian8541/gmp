using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.Shop.Request
{
    /// <summary>
    /// 获取门店统计信息
    /// </summary>
    public class GetShopStatisticsRequest : RequestBase
    {
        /// <summary>
        /// 省
        /// </summary>
        public long Province { get; set; }
    }
}
