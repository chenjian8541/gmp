using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.DataAccess.Common
{
    /// <summary>
    /// 数据访问接口约定
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDbContext<T> where T : DbContext, new()
    {
        /// <summary>
        /// 获取数据访问上下文
        /// </summary>
        /// <returns></returns>
        T GetDbContext();
    }
}
