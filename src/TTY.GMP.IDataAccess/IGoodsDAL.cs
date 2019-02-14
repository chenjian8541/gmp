using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.View;
using TTY.GMP.Entity.Web.Goods.Request;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 商品数据访问
    /// </summary>
    public interface IGoodsDAL
    {
        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        Task<Goods> GetGoods(long goodsId);

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        Task<GoodsView> GetGoodsView(long goodsId);

        /// <summary>
        /// 查询销售商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<SaleGoodsView>, int>> GetSaleGoodsPage(GetSaleGoodsPageRequest request);

        /// <summary>
        /// 查询商品销售记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<SaleGoodsLogView>, int>> GetSaleGoodsLogPage(GetSaleGoodsLogPageRequest request);

        /// <summary>
        /// 查询商品采购记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<ReceiptInLogView>, int>> GetReceiptInLogPage(ReceiptInLogPageRequest request);

        /// <summary>
        /// 获取仓库下多个商品的库存数量
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="stockId"></param>
        /// <returns></returns>
        Task<IEnumerable<StStockQtyView>> GetStStockQtyView(List<long> goodsId, long stockId);

        /// <summary>
        /// 获取库存信息
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="stockIds"></param>
        /// <returns></returns>
        Task<IEnumerable<StStockQtyView>> GetStStockQtyView(List<long> goodsId, List<long> stockIds);

        /// <summary>
        /// 通过ID获取商品类别信息
        /// </summary>
        /// <param name="goodsCategoryId"></param>
        /// <returns></returns>
        Task<List<GoodsCategory>> GetGoodsCategory(List<long> goodsCategoryId);

        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        Task<List<SysUnit>> GetUnits(List<long> units);

        /// <summary>
        /// 获取品牌信息
        /// </summary>
        /// <param name="brandIds"></param>
        /// <returns></returns>
        Task<List<GoodsBrand>> GetGoodsBrands(List<long> brandIds);
    }
}
