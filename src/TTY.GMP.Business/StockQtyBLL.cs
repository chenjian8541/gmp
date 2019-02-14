using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.View;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 库存业务访问
    /// </summary>
    public class StockQtyBLL : IStockQtyBLL
    {
        /// <summary>
        /// 库存业务访问
        /// </summary>
        private readonly IStockQtyDAL _stockQtyDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="stockQtyDal"></param>
        public StockQtyBLL(IStockQtyDAL stockQtyDal)
        {
            this._stockQtyDal = stockQtyDal;
        }

        /// <summary>
        /// 获取最近修改的库存信息
        /// </summary>
        /// <param name="lastUpdateTime"></param>
        /// <returns></returns>
        public async Task<List<DbStockGoodsView>> GetRecentModifyStockGoods(long lastUpdateTime)
        {
            return await this._stockQtyDal.GetRecentModifyStockGoods(lastUpdateTime);
        }
    }
}
