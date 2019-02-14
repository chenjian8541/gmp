using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Open.Request
{
    /// <summary>
    /// 获取门店信息
    /// </summary>
    public class GetShopPagingReqest : RequestPagingBase
    {
        /// <summary>
        /// 数据校验
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (PageSize > 100)
            {
                return false;
            }
            return base.Validate();
        }
    }
}
