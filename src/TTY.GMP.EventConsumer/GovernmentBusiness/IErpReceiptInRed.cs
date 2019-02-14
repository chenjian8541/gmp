using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.EventConsumer.GovernmentBusiness
{
    /// <summary>
    /// 采购撤销
    /// </summary>
    public interface IErpReceiptInRed
    {
        /// <summary>
        /// 处理采购撤销
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="inReceipt"></param>
        /// <param name="areas"></param>
        Task Process(Shop shop, PoInReceipt inReceipt, List<Area> areas);
    }
}
