using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Config;
using TTY.GMP.Entity.Database;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;
using System.Linq;
using TTY.GMP.LOG;

namespace TTY.GMP.EventConsumer.GovernmentBusiness.GuangDong
{
    /// <summary>
    /// 采购退货
    /// </summary>
    public class GdSaveReceilBackBillCheck : ISaveReceilBackBillCheck
    {
        /// <summary>
        /// 处理采购退货
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="poBackReceipt"></param>
        /// <param name="areas"></param>
        /// <returns></returns>
        public async Task Process(Shop shop, PoBackReceipt poBackReceipt, List<Area> areas)
        {
            //TODO 未实现采购退货逻辑
        }
    }
}
