using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Common
{
    /// <summary>
    /// 获取分页数据请求
    /// </summary>
    public class RequestPagingBase : RequestBase
    {
        /// <summary>
        /// 每页数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageCurrent { get; set; }

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            return PageSize > 0 && PageCurrent > 0;
        }
    }
}
