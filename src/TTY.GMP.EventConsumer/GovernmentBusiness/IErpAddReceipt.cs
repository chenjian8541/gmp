using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.EventConsumer.GovernmentBusiness
{
    /// <summary>
    /// 采购入库
    /// </summary>
    public interface IErpAddReceipt
    {
        /// <summary>
        /// 处理采购入库
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="inReceipt"></param>
        /// <param name="areas"></param>
        Task Process(Shop shop, PoInReceipt inReceipt, List<Area> areas);
    }
}
