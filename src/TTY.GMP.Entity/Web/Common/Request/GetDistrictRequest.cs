using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.Common.Request
{
    /// <summary>
    /// 获取区
    /// </summary>
    public class GetDistrictRequest : RequestBase
    {
        /// <summary>
        /// 城市
        /// </summary>
        public long City { get; set; }

        /// <summary>
        /// 数据较验
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            return City > 0;
        }
    }
}
