using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.Event.GMP
{
    /// <summary>
    /// 采购撤销
    /// </summary>
    public class ErpReceiptInRedEvent : TTY.Event.GMP.Event
    {
        /// <summary>
        /// 采购ID
        /// </summary>
        public long InId { get; set; }
    }
}
