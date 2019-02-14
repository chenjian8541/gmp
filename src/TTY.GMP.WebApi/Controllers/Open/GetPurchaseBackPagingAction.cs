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
    /// <summary>
    /// 采购退货信息
    /// </summary>
    public class GetPurchaseBackPagingAction
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
        /// 供应商业务
        /// </summary>
        private readonly IBaseSupplierBLL _baseSupplierBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="openDataBll"></param>
        /// <param name="goodsBll"></param>
        /// <param name="areaBll"></param>
        /// <param name="baseSupplierBll"></param>
        public GetPurchaseBackPagingAction(IOpenDataBLL openDataBll, IGoodsBLL goodsBll, IAreaBLL areaBll, IBaseSupplierBLL baseSupplierBll)
        {
            this._openDataBll = openDataBll;
            this._goodsBll = goodsBll;
            this._areaBll = areaBll;
            this._baseSupplierBll = baseSupplierBll;
        }

        /// <summary>
        /// 分页获取商品信息
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponsePagingBase> ProcessAction(HttpContext httpContext, GetPurchaseBackPagingRequest request)
        {
            if (!HandleRequest(httpContext, request))
            {
                return ResponsePagingBase.Success(new List<OpBackReceiptDetail>(), 0);
            }
            var result = await _openDataBll.GetBackReceiptDetail(request);
            if (result == null || result.Item2 == 0)
            {
                return ResponsePagingBase.Success(new List<OpBackReceiptDetail>(), 0);
            }
            var inReceipt = result.Item1.ToList();
            var goodsCategoryIds = new List<long>();
            var areaIds = new List<long>();
            var unitIds = new List<long>();
            var brandIds = new List<long>();
            var supplierIds = new List<long>();
            foreach (var g in inReceipt)
            {
                areaIds.Add(g.Province);
                areaIds.Add(g.City);
                areaIds.Add(g.District);
                areaIds.Add(g.Street);
                unitIds.Add(g.UnitId);
                goodsCategoryIds.Add(g.GoodsCategoryId);
                brandIds.Add(g.BrandId);
                supplierIds.Add(g.BackOfferId);
            }
            var areas = await _areaBll.GetArea(areaIds.Distinct().ToList());
            var goodsCategorys = await _goodsBll.GetGoodsCategory(goodsCategoryIds.Distinct().ToList());
            var units = await _goodsBll.GetUnits(unitIds.Distinct().ToList());
            var brands = await _goodsBll.GetGoodsBrands(brandIds.Distinct().ToList());
            var suppliers = await _baseSupplierBll.GetBaseSupplier(supplierIds.Distinct().ToList());
            var opReceiptDetail = inReceipt.Select(p => new OpBackReceiptDetail()
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
                BackCode = p.BackCode,
                BackDate = p.BackDate.ToDateTimeFromTimeStamp(),
                Qty = p.Qty,
                SupplierName = ComLib.GetSupplierName(p.BackOfferId, suppliers),
                UnitName = ComLib.GetUnitNmae(p.UnitId, units)
            });
            return ResponsePagingBase.Success(opReceiptDetail, result.Item2);
        }

        /// <summary>
        /// 处理请求参数
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        private bool HandleRequest(HttpContext httpContext, GetPurchaseBackPagingRequest request)
        {
            return ComLib.HandleRequest(httpContext, request);
        }
    }
}
