using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.View;
using TTY.GMP.Entity.Web.Goods.Request;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 商品业务访问
    /// </summary>
    public class GoodsBLL : IGoodsBLL
    {
        /// <summary>
        /// 商品数据访问
        /// </summary>
        private readonly IGoodsDAL _goodsDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="goodsDal"></param>
        public GoodsBLL(IGoodsDAL goodsDal)
        {
            this._goodsDal = goodsDal;
        }

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public async Task<Goods> GetGoods(long goodsId)
        {
            return await this._goodsDal.GetGoods(goodsId);
        }

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public async Task<GoodsView> GetGoodsView(long goodsId)
        {
            return await this._goodsDal.GetGoodsView(goodsId);
        }

        /// <summary>
        /// 查询销售商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<SaleGoodsView>, int>> GetSaleGoodsPage(GetSaleGoodsPageRequest request)
        {
            return await _goodsDal.GetSaleGoodsPage(request);
        }

        /// <summary>
        /// 查询商品销售记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<SaleGoodsLogView>, int>> GetSaleGoodsLogPage(GetSaleGoodsLogPageRequest request)
        {
            return await _goodsDal.GetSaleGoodsLogPage(request);
        }

        /// <summary>
        /// 查询商品采购记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<ReceiptInLogView>, int>> GetReceiptInLogPage(ReceiptInLogPageRequest request)
        {
            return await _goodsDal.GetReceiptInLogPage(request);
        }

        /// <summary>
        /// 获取仓库下多个商品的库存数量
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StStockQtyView>> GetStStockQtyView(List<long> goodsId, long stockId)
        {
            return await this._goodsDal.GetStStockQtyView(goodsId, stockId);
        }

        /// <summary>
        /// 获取库存信息
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="stockIds"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StStockQtyView>> GetStStockQtyView(List<long> goodsId, List<long> stockIds)
        {
            if (goodsId.Count == 0 || stockIds.Count == 0)
            {
                return new List<StStockQtyView>();
            }
            return await this._goodsDal.GetStStockQtyView(goodsId, stockIds);
        }

        /// <summary>
        /// 通过ID获取商品类别信息
        /// </summary>
        /// <param name="goodsCategoryId"></param>
        /// <returns></returns>
        public async Task<List<GoodsCategory>> GetGoodsCategory(List<long> goodsCategoryId)
        {
            if (goodsCategoryId == null || goodsCategoryId.Count == 0)
            {
                return new List<GoodsCategory>();
            }
            return await this._goodsDal.GetGoodsCategory(goodsCategoryId);
        }

        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        public async Task<List<SysUnit>> GetUnits(List<long> units)
        {
            if (units == null || units.Count == 0)
            {
                return new List<SysUnit>();
            }
            return await this._goodsDal.GetUnits(units);
        }

        /// <summary>
        /// 获取品牌信息
        /// </summary>
        /// <param name="brandIds"></param>
        /// <returns></returns>
        public async Task<List<GoodsBrand>> GetGoodsBrands(List<long> brandIds)
        {
            if (brandIds == null || brandIds.Count == 0)
            {
                return new List<GoodsBrand>();
            }
            return await this._goodsDal.GetGoodsBrands(brandIds);
        }
    }
}
