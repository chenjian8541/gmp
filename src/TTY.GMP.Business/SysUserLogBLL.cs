using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;
using TTY.GMP.Utility;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 系统操作日志业务访问
    /// </summary>
    public class SysUserLogBLL : ISysUserLogBLL
    {
        /// <summary>
        /// 操作日志数据访问
        /// </summary>
        private readonly ISysUserLogDAL _sysUserLogDal;

        /// <summary>
        /// 临时存储操作日志信息
        /// </summary>
        private static readonly List<SysUserLog> LogBuckets;

        /// <summary>
        /// 临时存储日志最大条数
        /// </summary>
        private const int MaxLogsCount = 100;

        /// <summary>
        /// 日志处理锁,避免多线程重复执行
        /// </summary>
        private static readonly object LogsLock = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserLogDal"></param>
        public SysUserLogBLL(ISysUserLogDAL sysUserLogDal)
        {
            this._sysUserLogDal = sysUserLogDal;
        }

        /// <summary>
        /// 添加系统操作日志
        /// </summary>
        /// <param name="sysUserLog"></param>
        public void AddSysUserLog(SysUserLog sysUserLog)
        {
            sysUserLog.LogId = PrimaryKeyHelper.Instance.CreateID();
            lock (LogsLock)
            {
                LogBuckets.Add(sysUserLog);
                if (LogBuckets.Count < MaxLogsCount)
                {
                    return;
                }
                _sysUserLogDal.AddUserLog(LogBuckets);
                LogBuckets.Clear();
            }
        }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SysUserLogBLL()
        {
            LogBuckets = new List<SysUserLog>();
        }
    }
}
