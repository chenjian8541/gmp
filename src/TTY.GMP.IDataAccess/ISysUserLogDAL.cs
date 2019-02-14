using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 用户操作日志数据访问
    /// </summary>
    public interface ISysUserLogDAL
    {
        /// <summary>
        /// 批量新增用户操作日志
        /// </summary>
        /// <param name="sysUserLogs"></param>
        /// <returns></returns>
        Task AddUserLog(List<SysUserLog> sysUserLogs);
    }
}