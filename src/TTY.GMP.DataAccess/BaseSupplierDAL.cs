using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.Database;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 供应商数据访问
    /// </summary>
    public class BaseSupplierDAL : IBaseSupplierDAL, IDbContext<ErpDbContext>
    {
        /// <summary>
        /// 获取数据访问上下文
        /// </summary>
        /// <returns></returns>
        public ErpDbContext GetDbContext()
        {
            return new ErpDbContext();
        }

        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public async Task<BaseSupplier> GetBaseSupplier(long supplierId)
        {
            return await this.Find<ErpDbContext, BaseSupplier>(supplierId);
        }

        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="supplierIds"></param>
        /// <returns></returns>
        public async Task<List<BaseSupplier>> GetBaseSupplier(List<long> supplierIds)
        {
            return await this.FindList<ErpDbContext, BaseSupplier>(p => supplierIds.Contains(p.supplier_id));
        }
    }
}
