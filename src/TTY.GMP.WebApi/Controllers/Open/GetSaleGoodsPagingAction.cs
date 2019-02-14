using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Open.Request;
using TTY.GMP.Entity.Open.View;
using TTY.GMP.IBusiness;
using TTY.GMP.Utility;
using TTY.GMP.WebApi.Common;

namespace TTY.GMP.WebApi.Controllers.Open
{
    public class GetSaleGoodsPagingAction
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
        /// 客户信息
        /// </summary>
        private readonly IRetailCustomerBLL _retailCustomerBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="openDataBll"></param>
        /// <param name="goodsBll"></param>
        /// <param name="areaBll"></param>
        /// <param name="retailCustomerBll"></param>
        public GetSaleGoodsPagingAction(IOpenDataBLL openDataBll, IGoodsBLL goodsBll, IAreaBLL areaBll,
            IRetailCustomerBLL retailCustomerBll)
        {
            this._openDataBll = openDataBll;
            this._goodsBll = goodsBll;
            this._areaBll = areaBll;
            this._retailCustomerBll = retailCustomerBll;
        }

        /// <summary>
        /// 分页获取商品信息
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponsePagingBase> ProcessAction(HttpContext httpContext, GetSaleGoodsPagingRequest request)
        {
            if (!HandleRequest(httpContext, request))
            {
                return ResponsePagingBase.Success(new List<OpRetailDetail>(), 0);
            }
            var result = await _openDataBll.GetRetailDetail(request);
            if (result == null || result.Item2 == 0)
            {
                return ResponsePagingBase.Success(new List<OpRetailDetail>(), 0);
            }
            var retail = result.Item1.ToList();
            var goodsCategoryIds = new List<long>();
            var areaIds = new List<long>();
            var unitIds = new List<long>();
            var brandIds = new List<long>();
            var retailId = new List<long>();
            foreach (var g in retail)
            {
                areaIds.Add(g.Province);
                areaIds.Add(g.City);
                areaIds.Add(g.District);
                areaIds.Add(g.Street);
                unitIds.Add(g.UnitId);
                goodsCategoryIds.Add(g.GoodsCategoryId);
                brandIds.Add(g.BrandId);
                retailId.Add(g.RetailId);
            }
            var areas = await _areaBll.GetArea(areaIds.Distinct().ToList());
            var goodsCategorys = await _goodsBll.GetGoodsCategory(goodsCategoryIds.Distinct().ToList());
            var units = await _goodsBll.GetUnits(unitIds.Distinct().ToList());
            var brands = await _goodsBll.GetGoodsBrands(brandIds.Distinct().ToList());
            var customers = await _retailCustomerBll.GetRetailCustomer(retailId.Distinct().ToList());
            var opRetailDetail = retail.Select(p =>
            {
                var customer = customers.FirstOrDefault(g => g.RetailId == p.RetailId);
                return new OpRetailDetail()
                {
                    Province = ComLib.GetAreaName(p.Province, areas),
                    City = ComLib.GetAreaName(p.City, areas),
                    District = ComLib.GetAreaName(p.District, areas),
                    Street = ComLib.GetAreaName(p.Street, areas),
                    GoodsCategoryName = ComLib.GetCategoryName(p.GoodsCategoryId, goodsCategorys),
                    GoodsName = p.GoodsName,
                    GoodsSpec = p.GoodsSpec,
                    ShopName = p.ShopName,
                    GoodsBrand = ComLib.GetBrand(p.BrandId, brands),
                    BillCode = p.BillCode,
                    BillDate = p.BillDate.ToDateTimeFromTimeStamp(),
                    Qty = p.Qty,
                    UnitName = ComLib.GetUnitNmae(p.UnitId, units),
                    BuyerIdentification = customer?.Identification,
                    BuyerName = customer?.RetailCustomerName,
                    BuyerTel = customer?.RetailCustomerTel
                };
            });
            return ResponsePagingBase.Success(opRetailDetail, result.Item2);
        }

        /// <summary>
        /// 处理请求参数
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        private bool HandleRequest(HttpContext httpContext, GetSaleGoodsPagingRequest request)
        {
            return ComLib.HandleRequest(httpContext, request);
        }
    }
}
