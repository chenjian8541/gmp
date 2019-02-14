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
    /// 商品出入库记录
    /// </summary>
    public class StockInOutRecordBLL : IStockInOutRecordBLL
    {
        /// <summary>
        /// 商品出入库数据访问
        /// </summary>
        private readonly IStockInOutRecordDAL _stockInOutRecorDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="stockInOutRecordDal"></param>
        public StockInOutRecordBLL(IStockInOutRecordDAL stockInOutRecordDal)
        {
            this._stockInOutRecorDal = stockInOutRecordDal;
        }

        /// <summary>
        /// 获取商品出入库统计
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="billTypes"></param>
        /// <returns></returns>
        public Task<List<StockInOutView>> GetStockInOut(long startTime, long endTime, string billTypes)
        {
            return this._stockInOutRecorDal.GetStockInOut(startTime, endTime, billTypes);
        }
    }
}
