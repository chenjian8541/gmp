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
    /// 供应商业务访问
    /// </summary>
    public class BaseSupplierBLL : IBaseSupplierBLL
    {
        /// <summary>
        /// 供应商数据访问
        /// </summary>
        private readonly IBaseSupplierDAL _baseSupplierDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="baseSupplierDal"></param>
        public BaseSupplierBLL(IBaseSupplierDAL baseSupplierDal)
        {
            this._baseSupplierDal = baseSupplierDal;
        }

        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public async Task<BaseSupplier> GetBaseSupplier(long supplierId)
        {
            return await this._baseSupplierDal.GetBaseSupplier(supplierId);
        }

        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="supplierIds"></param>
        /// <returns></returns>
        public async Task<List<BaseSupplier>> GetBaseSupplier(List<long> supplierIds)
        {
            if (supplierIds == null || supplierIds.Count == 0)
            {
                return new List<BaseSupplier>();
            }
            return await this._baseSupplierDal.GetBaseSupplier(supplierIds);
        }
    }
}
