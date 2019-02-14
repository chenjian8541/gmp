using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.View;

namespace TTY.GMP.IBusiness
{
    /// <summary>
    /// 零售订单业务访问层
    /// </summary>
    public interface IRetailBLL
    {
        /// <summary>
        /// 通过单据ID获取单据信息
        /// </summary>
        /// <param name="retailId"></param>
        /// <returns></returns>
        Task<SoRetail> GetSoRetail(long retailId);

        /// <summary>
        /// 通过单据ID获取单据详情信息
        /// </summary>
        /// <param name="retailId"></param>
        /// <returns></returns>
        Task<List<SoRetailDetailView>> GetSoRetailDetail(long retailId);

        /// <summary>
        /// 通过单据ID获取购买客户信息
        /// </summary>
        /// <param name="retailId"></param>
        /// <returns></returns>
        Task<CmRetailCustomer> GetCmRetailCustomer(long retailId);
    }
}
