using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.EventConsumer.GovernmentBusiness
{
    /// <summary>
    /// 采购退货
    /// </summary>
    public interface ISaveReceilBackBillCheck
    {
        /// <summary>
        /// 处理采购退货
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="poBackReceipt"></param>
        /// <param name="areas"></param>
        Task Process(Shop shop, PoBackReceipt poBackReceipt, List<Area> areas);
    }
}
