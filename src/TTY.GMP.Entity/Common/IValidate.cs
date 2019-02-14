using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Common
{
    /// <summary>
    /// 数据较验
    /// </summary>
    public interface IValidate
    {
        /// <summary>
        /// 对象的有效性验证方法
        /// </summary>
        /// <returns></returns>
        bool Validate();
    }
}
