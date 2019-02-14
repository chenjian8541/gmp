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
    /// 采购退货订单数据访问
    /// </summary>
    public class ReceiptBackDAL : IReceiptBackDAL, IDbContext<ErpDbContext>
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
        /// 获取采购退货订单
        /// </summary>
        /// <param name="backId"></param>
        /// <returns></returns>
        public async Task<PoBackReceipt> GetPoBackReceipt(long backId)
        {
            return await this.Find<ErpDbContext, PoBackReceipt>(backId);
        }

        /// <summary>
        /// 获取采购退货订单详情
        /// </summary>
        /// <param name="backId"></param>
        /// <returns></returns>
        public async Task<List<PoBackReceiptDetail>> GetPoBackReceiptDetail(long backId)
        {
            return await this.FindList<ErpDbContext, PoBackReceiptDetail>(p => p.back_id == backId);
        }
    }
}
