using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.Database;
using TTY.GMP.IDataAccess;
using TTY.GMP.LOG;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 用户操作日志数据访问
    /// </summary>
    public class SysUserLogDAL : ISysUserLogDAL, IDbContext<GmpDbContext>
    {
        /// <summary>
        /// 获取数据访问上下文
        /// </summary>
        /// <returns></returns>
        public GmpDbContext GetDbContext()
        {
            return new GmpDbContext();
        }

        /// <summary>
        /// 批量新增用户操作日志
        /// </summary>
        /// <param name="sysUserLogs"></param>
        /// <returns></returns>
        public async Task AddUserLog(List<SysUserLog> sysUserLogs)
        {
            try
            {
                await this.InsertRange(sysUserLogs);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this.GetType());
                Log.Warn($"用户操作日志:{JsonConvert.SerializeObject(sysUserLogs)}", this.GetType());
            }
        }
    }
}
