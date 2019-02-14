using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.Open.Request
{
    /// <summary>
    /// 获取门店销售单排名信息(成长之星)
    /// </summary>
    public class GetShopRetailRankAboutGrowthRequest : RequestBase
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 数据较验
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            return ShopId > 0;
        }
    }
}
