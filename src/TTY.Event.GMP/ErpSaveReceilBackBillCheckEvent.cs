using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.Event.GMP
{
    /// <summary>
    /// 采购退货单据
    /// </summary>
    public class ErpSaveReceilBackBillCheckEvent : Event
    {
        /// <summary>
        /// 退货单ID
        /// </summary>
        public long BackId { get; set; }
    }
}
