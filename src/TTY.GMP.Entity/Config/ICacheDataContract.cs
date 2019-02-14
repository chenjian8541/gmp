using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Config
{
    /// <summary>
    /// 缓存数据约定
    /// </summary>
    public interface ICacheDataContract
    {
        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        string GetKeyFormat(params object[] parms);
    }
}
