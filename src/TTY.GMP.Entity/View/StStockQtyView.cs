using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.View
{
    /// <summary>
    /// 商品库存信息
    /// </summary>
    public class StStockQtyView
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long GoodsId { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public long StockId { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public decimal Qty { get; set; }
    }
}
