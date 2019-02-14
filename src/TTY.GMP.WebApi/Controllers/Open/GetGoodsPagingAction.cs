using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Open.Request;
using TTY.GMP.Entity.Open.View;
using TTY.GMP.Entity.View;
using TTY.GMP.IBusiness;
using TTY.GMP.WebApi.Common;

namespace TTY.GMP.WebApi.Controllers.Open
{
    /// <summary>
    /// 分页获取商品信息
    /// </summary>
    public class GetGoodsPagingAction
    {
        /// <summary>
        /// 开发数据业务访问
        /// </summary>
        private readonly IOpenDataBLL _openDataBll;

        /// <summary>
        /// 商品信息
        /// </summary>
        private readonly IGoodsBLL _goodsBll;

        /// <summary>
        /// 地区信息
        /// </summary>
        private readonly IAreaBLL _areaBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="openDataBll"></param>
        /// <param name="goodsBll"></param>
        /// <param name="areaBll"></param>
        public GetGoodsPagingAction(IOpenDataBLL openDataBll, IGoodsBLL goodsBll, IAreaBLL areaBll)
        {
            this._openDataBll = openDataBll;
            this._goodsBll = goodsBll;
            this._areaBll = areaBll;
        }

        /// <summary>
        /// 分页获取商品信息
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponsePagingBase> ProcessAction(HttpContext httpContext, GetGoodsPagingRequest request)
        {
            if (!HandleRequest(httpContext, request))
            {
                return ResponsePagingBase.Success(new List<OpGoodsView>(), 0);
            }
            var result = await _openDataBll.GetGoodsPaging(request);
            if (result == null || result.Item2 == 0)
            {
                return ResponsePagingBase.Success(new List<OpGoodsView>(), 0);
            }
            var goods = result.Item1.ToList();
            var goodsIds = new List<long>();
            var goodsCategoryIds = new List<long>();
            var stockIds = new List<long>();
            var areaIds = new List<long>();
            var unitIds = new List<long>();
            var brandIds = new List<long>();
            foreach (var g in goods)
            {
                goodsIds.Add(g.GoodsId);
                stockIds.Add(g.StockId);
                areaIds.Add(g.Province);
                areaIds.Add(g.City);
                areaIds.Add(g.District);
                areaIds.Add(g.Street);
                unitIds.Add(g.BaseUnitId);
                goodsCategoryIds.Add(g.GoodsCategoryId);
                brandIds.Add(g.BrandId);
            }
            var areas = await _areaBll.GetArea(areaIds.Distinct().ToList());
            var goodsCategorys = await _goodsBll.GetGoodsCategory(goodsCategoryIds.Distinct().ToList());
            var units = await _goodsBll.GetUnits(unitIds.Distinct().ToList());
            var stockQtys = await _goodsBll.GetStStockQtyView(goodsIds, stockIds);
            var brands = await _goodsBll.GetGoodsBrands(brandIds.Distinct().ToList());
            var opGoodsView = goods.Select(p => new OpGoodsView()
            {
                Province = ComLib.GetAreaName(p.Province, areas),
                City = ComLib.GetAreaName(p.City, areas),
                District = ComLib.GetAreaName(p.District, areas),
                Street = ComLib.GetAreaName(p.Street, areas),
                Comment = p.Comment,
                Contents = p.Contents,
                DosageForms = p.DosageForms,
                GoodsCategoryName = ComLib.GetCategoryName(p.GoodsCategoryId, goodsCategorys),
                GoodsCode = p.GoodsCode,
                GoodsIngredient = p.GoodsIngredient,
                GoodsName = p.GoodsName,
                GoodsOrigin = p.GoodsOrigin,
                GoodsProduct = p.GoodsProduct,
                GoodsRestrictive = p.GoodsRestrictive,
                GoodsShortName = p.GoodsShortName,
                GoodsSpec = p.GoodsSpec,
                RegistrationHolder = p.RegistrationHolder,
                RegistrationNumber = p.RegistrationNumber,
                ShopName = p.ShopName,
                RepertoryUnit = ComLib.GetUnitNmae(p.BaseUnitId, units),
                RepertoryQty = ComLib.GetRepertoryQty(p.GoodsId, p.StockId, stockQtys),
                GoodsBrand = ComLib.GetBrand(p.BrandId, brands)
            });
            return ResponsePagingBase.Success(opGoodsView, result.Item2);
        }

        /// <summary>
        /// 处理请求参数
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        private bool HandleRequest(HttpContext httpContext, GetGoodsPagingRequest request)
        {
            return ComLib.HandleRequest(httpContext, request);
        }
    }
}
