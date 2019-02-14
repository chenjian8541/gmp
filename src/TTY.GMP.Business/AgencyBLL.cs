using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 经销商业务访问
    /// </summary>
    public class AgencyBLL : IAgencyBLL
    {
        /// <summary>
        /// 代理商数据访问
        /// </summary>
        private readonly IAgencyDAL _agencyDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="agencyDal"></param>
        public AgencyBLL(IAgencyDAL agencyDal)
        {
            this._agencyDal = agencyDal;
        }

        /// <summary>
        /// 添加经销商
        /// </summary>
        /// <param name="agency"></param>
        public async Task AddAgency(Agency agency)
        {
            await _agencyDal.AddAgency(agency);
        }

        /// <summary>
        /// 获取经销商信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<Agency> GetAgency(long shopId)
        {
            return await _agencyDal.GetAgency(shopId);
        }
    }
}
