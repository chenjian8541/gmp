using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.View;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 客户信息
    /// </summary>
    public interface IRetailCustomerDAL
    {
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="retailIds"></param>
        /// <returns></returns>
        Task<List<DbRetailCustomerView>> GetRetailCustomer(List<long> retailIds);
    }
}
