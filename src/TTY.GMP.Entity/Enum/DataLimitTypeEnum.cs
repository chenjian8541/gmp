using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Enum
{
    /// <summary>
    /// 数据权限设置类型
    /// </summary>
    public enum DataLimitTypeEnum
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,

        /// <summary>
        /// 指定区域(默认)
        /// </summary>
        Area = 1,

        /// <summary>
        /// 指定门店
        /// </summary>
        Shop = 2
    }
}
