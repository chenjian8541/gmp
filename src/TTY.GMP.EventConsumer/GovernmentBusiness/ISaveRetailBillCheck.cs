using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.EventConsumer.GovernmentBusiness
{
    /// <summary>
    /// 零售下单
    /// </summary>
    public interface ISaveRetailBillCheck
    {
        /// <summary>
        /// 处理零售下单
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="soRetail"></param>
        /// <param name="areas"></param>
        Task Process(Shop shop, SoRetail soRetail, List<Area> areas);
    }
}
