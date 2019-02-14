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
    /// 销售退货单数据访问
    /// </summary>
    public class RetailBackDAL : IRetailBackDAL, IDbContext<ErpDbContext>
    {
        /// <summary>
        /// 获取数据访问上下文
        /// </summary>
        /// <returns></returns>
        public ErpDbContext GetDbContext()
        {
            return new ErpDbContext();
        }

        /// <summary>
        /// 获取销售退货单
        /// </summary>
        /// <param name="retailBackId"></param>
        /// <returns></returns>
        public async Task<SoRetailBack> GetSoRetailBack(long retailBackId)
        {
            return await this.Find<ErpDbContext, SoRetailBack>(retailBackId);
        }

        /// <summary>
        /// 获取销售退货单详情
        /// </summary>
        /// <param name="retailBackId"></param>
        /// <returns></returns>
        public async Task<List<SoRetailBackDetail>> GetSoRetailBackDetail(long retailBackId)
        {
            return await this.FindList<ErpDbContext, SoRetailBackDetail>(p => p.retail_back_id == retailBackId);
        }
    }
}
