using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Open.database;
using TTY.GMP.Entity.View;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 开放数据
    /// </summary>
    public class OpenDataBLL : IOpenDataBLL
    {
        /// <summary>
        /// 数据访问层
        /// </summary>
        private readonly IOpenDataDAL _openDataDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="openDataDal"></param>
        public OpenDataBLL(IOpenDataDAL openDataDal)
        {
            this._openDataDal = openDataDal;
        }

        /// <summary>
        /// 分页获取商品及库存信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<DbGoodsView>, int>> GetGoodsPaging<T>(T request) where T : RequestPagingBase, IQuerySql
        {
            return await this._openDataDal.GetGoodsPaging(request);
        }

        /// <summary>
        /// 分页获取商品采购信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<InReceiptDetail>, int>> GetInReceiptDetail<T>(T request)
            where T : RequestPagingBase, IQuerySql
        {
            return await this._openDataDal.GetInReceiptDetail(request);
        }

        /// <summary>
        /// 分页获取商品采购退货信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<BackReceiptDetail>, int>> GetBackReceiptDetail<T>(T request)
            where T : RequestPagingBase, IQuerySql
        {
            return await this._openDataDal.GetBackReceiptDetail(request);
        }

        /// <summary>
        /// 分页获取商品销售信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<RetailDetail>, int>> GetRetailDetail<T>(T request)
            where T : RequestPagingBase, IQuerySql
        {
            return await this._openDataDal.GetRetailDetail(request);
        }

        /// <summary>
        /// 分页获取商品销售退货信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<RetailBackDetail>, int>> GetRetailBackDetail<T>(T request)
            where T : RequestPagingBase, IQuerySql
        {
            return await this._openDataDal.GetRetailBackDetail(request);
        }
    }
}
