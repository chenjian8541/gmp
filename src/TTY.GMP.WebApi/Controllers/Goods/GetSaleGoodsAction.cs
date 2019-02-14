using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.View;
using TTY.GMP.Entity.Web.Goods.Request;
using TTY.GMP.IBusiness;

namespace TTY.GMP.WebApi.Controllers.Goods
{
    /// <summary>
    /// 获取销售商品信息
    /// </summary>
    public class GetSaleGoodsAction
    {
        /// <summary>
        /// 店面业务逻辑
        /// </summary>
        private readonly IShopBLL _shopBll;

        /// <summary>
        /// 商品业务逻辑
        /// </summary>
        private readonly IGoodsBLL _goodsBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopBll"></param>
        /// <param name="goodsBll"></param>
        public GetSaleGoodsAction(IShopBLL shopBll, IGoodsBLL goodsBll)
        {
            this._shopBll = shopBll;
            this._goodsBll = goodsBll;
        }

        /// <summary>
        /// 分页获取销售商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponsePagingBase> ProcessAction(GetSaleGoodsPageRequest request)
        {
            var shop = await _shopBll.GetShop(request.ShopId);
            if (shop == null)
            {
                return new ResponsePagingBase().GetResponseError(StatusCode.Shop50001, "门店不存在");
            }
            request.OrgId = shop.OrgId;
            request.StockId = shop.StockId;
            var result = await _goodsBll.GetSaleGoodsPage(request);
            return ResponsePagingBase.Success(await HandleQty(result.Item1.ToList(), shop.StockId), result.Item2);
        }

        /// <summary>
        /// 处理库存信息
        /// </summary>
        /// <param name="saleGoodsViews"></param>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public async Task<List<SaleGoodsView>> HandleQty(List<SaleGoodsView> saleGoodsViews, long stockId)
        {
            if (saleGoodsViews == null || !saleGoodsViews.Any())
            {
                return saleGoodsViews;
            }
            var goodsIds = saleGoodsViews.Select(p => p.GoodsId).ToList();
            var qtys = await _goodsBll.GetStStockQtyView(goodsIds, stockId);
            saleGoodsViews.ForEach(p =>
            {
                var qtyView = qtys.FirstOrDefault(j => j.GoodsId == p.GoodsId);
                if (qtyView != null)
                {
                    p.Qty = qtyView.Qty;
                }
            });
            return saleGoodsViews;
        }
    }
}
