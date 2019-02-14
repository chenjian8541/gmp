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
    /// 销售退货单业务访问
    /// </summary>
    public class RetailBackBLL : IRetailBackBLL
    {
        /// <summary>
        /// 销售退货单数据访问
        /// </summary>
        private readonly IRetailBackDAL _retailBackDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="retailBackDal"></param>
        public RetailBackBLL(IRetailBackDAL retailBackDal)
        {
            this._retailBackDal = retailBackDal;
        }

        /// <summary>
        /// 获取销售退货单
        /// </summary>
        /// <param name="retailBackId"></param>
        /// <returns></returns>
        public async Task<SoRetailBack> GetSoRetailBack(long retailBackId)
        {
            return await this._retailBackDal.GetSoRetailBack(retailBackId);
        }

        /// <summary>
        /// 获取销售退货单详情
        /// </summary>
        /// <param name="retailBackId"></param>
        /// <returns></returns>
        public async Task<List<SoRetailBackDetail>> GetSoRetailBackDetail(long retailBackId)
        {
            return await this._retailBackDal.GetSoRetailBackDetail(retailBackId);
        }
    }
}
