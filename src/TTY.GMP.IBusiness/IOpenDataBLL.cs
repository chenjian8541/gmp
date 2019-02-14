using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Open.database;
using TTY.GMP.Entity.View;

namespace TTY.GMP.IBusiness
{
    /// <summary>
    /// 开放数据
    /// </summary>
    public interface IOpenDataBLL
    {
        /// <summary>
        /// 分页获取商品及库存信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<DbGoodsView>, int>> GetGoodsPaging<T>(T request) where T : RequestPagingBase, IQuerySql;

        /// <summary>
        /// 分页获取商品采购信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<InReceiptDetail>, int>> GetInReceiptDetail<T>(T request)
           where T : RequestPagingBase, IQuerySql;

        /// <summary>
        /// 分页获取商品采购退货信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<BackReceiptDetail>, int>> GetBackReceiptDetail<T>(T request)
           where T : RequestPagingBase, IQuerySql;

        /// <summary>
        /// 分页获取商品销售信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<RetailDetail>, int>> GetRetailDetail<T>(T request)
           where T : RequestPagingBase, IQuerySql;

        /// <summary>
        /// 分页获取商品销售退货信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<RetailBackDetail>, int>> GetRetailBackDetail<T>(T request)
            where T : RequestPagingBase, IQuerySql;
    }
}
