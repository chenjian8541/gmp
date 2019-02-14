using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTY.Event.GMP
{
    /// <summary>
    /// 零售保存单据过账
    /// </summary>
    public class ErpSaveRetailBillCheckEvent : Event
    {
        /// <summary>
        ///  单据ID
        /// </summary>
        public long RetailId { get; set; }
    }
}
