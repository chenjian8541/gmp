using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.Common.Request
{
    /// <summary>
    /// 获取城市信息
    /// </summary>
    public class GetCityRequest : RequestBase
    {
        /// <summary>
        /// 省
        /// </summary>
        public long Province { get; set; }

        /// <summary>
        /// 数据较验
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            return Province > 0;
        }
    }
}
