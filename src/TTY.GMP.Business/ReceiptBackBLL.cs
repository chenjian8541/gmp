using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 采购退货单业务逻辑
    /// </summary>
    public class ReceiptBackBLL : IReceiptBackBLL
    {
        /// <summary>
        /// 采购退货单数据访问
        /// </summary>
        private readonly IReceiptBackDAL _receiptBackDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="receiptBackDal"></param>
        public ReceiptBackBLL(IReceiptBackDAL receiptBackDal)
        {
            this._receiptBackDal = receiptBackDal;
        }

        /// <summary>
        /// 获取采购退货订单
        /// </summary>
        /// <param name="backId"></param>
        /// <returns></returns>
        public async Task<PoBackReceipt> GetPoBackReceipt(long backId)
        {
            return await this._receiptBackDal.GetPoBackReceipt(backId);
        }

        /// <summary>
        /// 获取采购退货订单详情
        /// </summary>
        /// <param name="backId"></param>
        /// <returns></returns>
        public async Task<List<PoBackReceiptDetail>> GetPoBackReceiptDetail(long backId)
        {
            return await this._receiptBackDal.GetPoBackReceiptDetail(backId);
        }
    }
}
