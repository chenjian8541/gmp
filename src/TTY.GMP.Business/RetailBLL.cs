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
    /// 零售订单业务访问层
    /// </summary>
    public class RetailBLL : IRetailBLL
    {
        /// <summary>
        /// 零售订单数据访问
        /// </summary>
        private readonly IRetailDAL _retailDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="retailDal"></param>
        public RetailBLL(IRetailDAL retailDal)
        {
            this._retailDal = retailDal;
        }

        /// <summary>
        /// 通过单据ID获取单据信息
        /// </summary>
        /// <param name="retailId"></param>
        /// <returns></returns>
        public async Task<SoRetail> GetSoRetail(long retailId)
        {
            return await this._retailDal.GetSoRetail(retailId);
        }

        /// <summary>
        /// 通过单据ID获取单据详情信息
        /// </summary>
        /// <param name="retailId"></param>
        /// <returns></returns>
        public async Task<List<SoRetailDetailView>> GetSoRetailDetail(long retailId)
        {
            return await this._retailDal.GetSoRetailDetail(retailId);
        }

        /// <summary>
        /// 通过单据ID获取购买客户信息
        /// </summary>
        /// <param name="retailId"></param>
        /// <returns></returns>
        public async Task<CmRetailCustomer> GetCmRetailCustomer(long retailId)
        {
            return await this._retailDal.GetCmRetailCustomer(retailId);
        }
    }
}
