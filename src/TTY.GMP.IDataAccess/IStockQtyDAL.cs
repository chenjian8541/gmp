using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.View;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 库存数据访问
    /// </summary>
    public interface IStockQtyDAL
    {
        /// <summary>
        /// 获取最近修改的库存信息
        /// </summary>
        /// <param name="lastUpdateTime"></param>
        /// <returns></returns>
        Task<List<DbStockGoodsView>> GetRecentModifyStockGoods(long lastUpdateTime);
    }
}
