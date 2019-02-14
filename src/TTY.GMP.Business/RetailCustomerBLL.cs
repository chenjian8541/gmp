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
    /// 客户信息
    /// </summary>
    public class RetailCustomerBLL : IRetailCustomerBLL
    {
        /// <summary>
        /// 客户信息数据访问
        /// </summary>
        private readonly IRetailCustomerDAL _retailCustomerDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="retailCustomerDal"></param>
        public RetailCustomerBLL(IRetailCustomerDAL retailCustomerDal)
        {
            this._retailCustomerDal = retailCustomerDal;
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="retailIds"></param>
        /// <returns></returns>
        public async Task<List<DbRetailCustomerView>> GetRetailCustomer(List<long> retailIds)
        {
            if (retailIds == null || retailIds.Count == 0)
            {
                return new List<DbRetailCustomerView>();
            }
            return await this._retailCustomerDal.GetRetailCustomer(retailIds);
        }
    }
}
