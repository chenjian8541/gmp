using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.Event.GMP
{
    /// <summary>
    /// 零售退货单过账
    /// </summary>
    public class ErpSavaRetailBackBillCheckEvent : Event
    {
        /// <summary>
        /// 退货单ID
        /// </summary>
        public long RetailBackId { get; set; }
    }
}
