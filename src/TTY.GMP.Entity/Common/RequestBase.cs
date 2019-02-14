using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Common
{
    /// <summary>
    /// 请求基类
    /// </summary>
    public class RequestBase : IValidate
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long LoginUserId { set; get; }

        /// <summary>
        /// 数据较验
        /// </summary>
        /// <returns></returns>
        public virtual bool Validate()
        {
            return true;
        }
    }
}
