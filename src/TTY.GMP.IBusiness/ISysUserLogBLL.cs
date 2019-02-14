using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.IBusiness
{
    /// <summary>
    /// 系统操作日志业务访问
    /// </summary>
    public interface ISysUserLogBLL
    {
        /// <summary>
        /// 添加系统操作日志
        /// </summary>
        /// <param name="sysUserLog"></param>
        void AddSysUserLog(SysUserLog sysUserLog);
    }
}
