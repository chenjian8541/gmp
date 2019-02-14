using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.IBusiness
{
    /// <summary>
    /// 经销商业务访问
    /// </summary>
    public interface IAgencyBLL
    {
        /// <summary>
        /// 添加经销商
        /// </summary>
        /// <param name="agency"></param>
        Task AddAgency(Agency agency);

        /// <summary>
        /// 获取经销商信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<Agency> GetAgency(long shopId);
    }
}
