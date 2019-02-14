using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.View;

namespace TTY.GMP.IBusiness
{
    /// <summary>
    /// 商品出入库记录
    /// </summary>
    public interface IStockInOutRecordBLL
    {
        /// <summary>
        /// 获取商品出入库统计
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="billTypes"></param>
        /// <returns></returns>
        Task<List<StockInOutView>> GetStockInOut(long startTime, long endTime, string billTypes);
    }
}
