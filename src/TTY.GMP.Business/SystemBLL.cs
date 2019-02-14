using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.View;
using TTY.GMP.Entity.Web.Common.Request;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 系统数据访问
    /// </summary>
    public class SystemBLL : ISystemBLL
    {
        /// <summary>
        /// 系统数据访问
        /// </summary>
        private ISystemDAL _systemDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="systemDal"></param>
        public SystemBLL(ISystemDAL systemDal)
        {
            this._systemDal = systemDal;
        }

        /// <summary>
        /// 获取系统统计数据
        /// </summary>
        /// <returns></returns>
        /// <param name="request"></param>
        public async Task<SystemStatisticsView> GetSystemStatistics(GetSystemStatisticsRequest request)
        {
            return await this._systemDal.GetSystemStatistics(request);
        }
    }
}
