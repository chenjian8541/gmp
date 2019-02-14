using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.View;
using TTY.GMP.Entity.Web.Common.Request;

namespace TTY.GMP.IBusiness
{
    /// <summary>
    /// 系统数据访问
    /// </summary>
    public interface ISystemBLL
    {
        /// <summary>
        /// 获取系统统计数据
        /// </summary>
        /// <returns></returns>
        /// <param name="request"></param>
        Task<SystemStatisticsView> GetSystemStatistics(GetSystemStatisticsRequest request);
    }
}
