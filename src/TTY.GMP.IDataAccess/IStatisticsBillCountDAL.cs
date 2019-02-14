using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.View;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 订单数统计
    /// </summary>
    public interface IStatisticsBillCountDAL
    {
        /// <summary>
        /// 获取零售单数统计(包括商品零售、零售退货)
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<List<DbStatisticsRetailBillCount>> GetAllRetailBillCount(long startTime, long endTime);

        /// <summary>
        /// 获取采购单数(采购、采购退货)
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<List<DbStatisticsPurchaseBillCount>> GetAllPurchaseBillCount(long startTime, long endTime);
    }
}
