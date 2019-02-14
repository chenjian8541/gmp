using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Common
{
    /// <summary>
    /// 查询SQL
    /// </summary>
    public interface IQuerySql
    {
        /// <summary>
        /// 获取条件查询语句
        /// </summary>
        /// <returns></returns>
        string GetQuerySql();
    }
}
