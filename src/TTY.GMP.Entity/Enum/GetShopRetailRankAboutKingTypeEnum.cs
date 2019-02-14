using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Enum
{
    /// <summary>
    /// 门店销售单排名信息(订单王) 类别
    /// </summary>
    public enum GetShopRetailRankAboutKingTypeEnum
    {
        /// <summary>
        /// 订单数不够(0＜门店销售单据数＜2000)
        /// </summary>
        NotEnough = 1,

        /// <summary>
        /// 订单数足够 (门店销售单据数≥2000)
        /// </summary>
        Enough = 2,

        /// <summary>
        /// 订单数为0 (门店销售单据数=0)
        /// </summary>
        Zero = 3,
    }
}
