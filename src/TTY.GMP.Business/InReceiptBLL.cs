using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.View;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 进货单业务访问
    /// </summary>
    public class InReceiptBLL : IInReceiptBLL
    {
        /// <summary>
        /// 进货单数据访问
        /// </summary>
        private readonly IInReceiptDAL _inReceiptDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inReceiptDal"></param>
        public InReceiptBLL(IInReceiptDAL inReceiptDal)
        {
            this._inReceiptDal = inReceiptDal;
        }

        /// <summary>
        /// 获取进货单信息
        /// </summary>
        /// <param name="inId"></param>
        /// <returns></returns>
        public async Task<PoInReceipt> GetPoInReceipt(long inId)
        {
            return await this._inReceiptDal.GetPoInReceipt(inId);
        }

        /// <summary>
        /// 获取进货单详情
        /// </summary>
        /// <param name="inId"></param>
        /// <returns></returns>
        public async Task<List<PoInReceiptDetail>> GetPoInReceiptDetail(long inId)
        {
            return await this._inReceiptDal.GetPoInReceiptDetail(inId);
        }

        /// <summary>
        /// 通过单据ID获取单据详情信息
        /// </summary>
        /// <param name="inId"></param>
        /// <returns></returns>
        public async Task<List<PoInReceiptDetailView>> GetPoInReceiptDetailView(long inId)
        {
            return await this._inReceiptDal.GetPoInReceiptDetailView(inId);
        }
    }
}
