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
    /// 订单数统计
    /// </summary>
    public class StatisticsBillCountBLL : IStatisticsBillCountBLL
    {
        /// <summary>
        /// 订单数统计数据访问
        /// </summary>
        private readonly IStatisticsBillCountDAL _statisticsBillCountDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="statisticsBillCountDal"></param>
        public StatisticsBillCountBLL(IStatisticsBillCountDAL statisticsBillCountDal)
        {
            this._statisticsBillCountDal = statisticsBillCountDal;
        }

        /// <summary>
        /// 获取零售单数统计(包括商品零售、零售退货)
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<DbStatisticsRetailBillCount>> GetAllRetailBillCount(long startTime, long endTime)
        {
            return await this._statisticsBillCountDal.GetAllRetailBillCount(startTime, endTime);
        }

        /// <summary>
        /// 获取采购单数(采购、采购退货)
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<List<DbStatisticsPurchaseBillCount>> GetAllPurchaseBillCount(long startTime, long endTime)
        {
            return await this._statisticsBillCountDal.GetAllPurchaseBillCount(startTime, endTime);
        }
    }
}
