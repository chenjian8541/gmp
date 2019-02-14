using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 采购退货订单数据访问
    /// </summary>
    public interface IReceiptBackDAL
    {
        /// <summary>
        /// 获取采购退货订单
        /// </summary>
        /// <param name="backId"></param>
        /// <returns></returns>
        Task<PoBackReceipt> GetPoBackReceipt(long backId);

        /// <summary>
        /// 获取采购退货订单详情
        /// </summary>
        /// <param name="backId"></param>
        /// <returns></returns>
        Task<List<PoBackReceiptDetail>> GetPoBackReceiptDetail(long backId);
    }
}
