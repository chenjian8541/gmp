using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.EventConsumer.GovernmentBusiness
{
    /// <summary>
    /// 零售退货
    /// </summary>
    public interface ISavaRetailBackBillCheck
    {
        /// <summary>
        /// 处理零售退货
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="soRetailBack"></param>
        /// <param name="areas"></param>
        Task Process(Shop shop, SoRetailBack soRetailBack, List<Area> areas);
    }
}
