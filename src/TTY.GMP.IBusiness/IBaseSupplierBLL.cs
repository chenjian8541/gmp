using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.IBusiness
{
    /// <summary>
    /// 供应商业务访问
    /// </summary>
    public interface IBaseSupplierBLL
    {
        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        Task<BaseSupplier> GetBaseSupplier(long supplierId);

        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="supplierIds"></param>
        /// <returns></returns>
        Task<List<BaseSupplier>> GetBaseSupplier(List<long> supplierIds);
    }
}
