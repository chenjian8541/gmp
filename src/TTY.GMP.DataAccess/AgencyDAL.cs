using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.Database;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 经销商数据访问
    /// </summary>
    public class AgencyDAL : IAgencyDAL, IDbContext<GmpDbContext>
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
        /// 添加经销商
        /// </summary>
        /// <param name="agency"></param>
        public async Task AddAgency(Agency agency)
        {
            await this.Insert(agency);
        }

        /// <summary>
        /// 获取经销商信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<Agency> GetAgency(long shopId)
        {
            return await this.Find<GmpDbContext, Agency>(p => p.ShopId == shopId);
        }
    }
}
