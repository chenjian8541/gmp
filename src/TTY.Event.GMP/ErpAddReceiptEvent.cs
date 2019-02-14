using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTY.Event.GMP
{
    /// <summary>
    /// 采购入库
    /// </summary>
    public class ErpAddReceiptEvent : Event
    {
        /// <summary>
        /// 采购入库
        /// </summary>
        public long InId { get; set; }
    }
}
