using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using TTY.GMP.DataCore;

namespace TTY.GMP.DataAccess.Common
{
    /// <summary>
    /// 使用ADO.NET契约
    /// </summary>
    public interface IAdoContract
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="sqlHelper"></param>
        /// <returns></returns>
        DbConnection GetDbConnection(out ISqlHelper sqlHelper);
    }
}
