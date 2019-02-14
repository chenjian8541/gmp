using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Enum
{
    /// <summary>
    /// 获取门店销售单排名信息(成长之星) 类型
    /// </summary>
    public enum GetShopRetailRankAboutGrowthTypeEnum
    {
        /// <summary>
        /// 订单数不够(0＜门店销售单据数＜150)
        /// </summary>
        NotEnough = 1,

        /// <summary>
        /// 订单数足够 (门店销售单据数≥150)
        /// </summary>
        Enough = 2,

        /// <summary>
        /// 订单数为0 (门店销售单据数=0)
        /// </summary>
        Zero = 3,

        /// <summary>
        /// 已获奖 (该门店已获得过成长之星)
        /// </summary>
        IsWinner = 4
    }
}
