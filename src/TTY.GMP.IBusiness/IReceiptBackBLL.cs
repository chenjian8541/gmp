using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.IBusiness
{
    /// <summary>
    /// 采购退货单业务逻辑
    /// </summary>
    public interface IReceiptBackBLL
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
