using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.View;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 进货单数据访问
    /// </summary>
    public interface IInReceiptDAL
    {
        /// <summary>
        /// 获取进货单信息
        /// </summary>
        /// <param name="inId"></param>
        /// <returns></returns>
        Task<PoInReceipt> GetPoInReceipt(long inId);

        /// <summary>
        /// 获取进货单详情
        /// </summary>
        /// <param name="inId"></param>
        /// <returns></returns>
        Task<List<PoInReceiptDetail>> GetPoInReceiptDetail(long inId);

        /// <summary>
        /// 通过单据ID获取单据详情信息
        /// </summary>
        /// <param name="inId"></param>
        /// <returns></returns>
        Task<List<PoInReceiptDetailView>> GetPoInReceiptDetailView(long inId);
    }
}
