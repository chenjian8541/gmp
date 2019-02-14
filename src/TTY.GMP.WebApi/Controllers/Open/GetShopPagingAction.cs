using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Open.Request;
using TTY.GMP.Entity.Open.View;
using TTY.GMP.Entity.Platform.Response;
using TTY.GMP.Entity.Web.Shop.Request;
using TTY.GMP.Entity.Web.Shop.Response;
using TTY.GMP.IBusiness;
using TTY.GMP.WebApi.Common;

namespace TTY.GMP.WebApi.Controllers.Open
{
    public class GetShopPagingAction
    {
        /// <summary>
        /// 店面业务逻辑
        /// </summary>
        private readonly IShopBLL _shopBll;

        /// <summary>
        /// 地区业务访问
        /// </summary>
        private readonly IAreaBLL _areaBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopBll"></param>
        /// <param name="areaBll"></param>
        public GetShopPagingAction(IShopBLL shopBll, IAreaBLL areaBll)
        {
            this._shopBll = shopBll;
            this._areaBll = areaBll;
        }

        /// <summary>
        /// 通过地区获取店面信息
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponsePagingBase> ProcessAction(HttpContext httpContext, GetShopPagingReqest request)
        {
            var shopRequest = new GetShopPageRequest()
            {
                LoginUserId = request.LoginUserId,
                PageCurrent = request.PageCurrent,
                PageSize = request.PageSize,

            };
            if (!ComLib.HandleRequest(httpContext, shopRequest))
            {
                return ResponsePagingBase.Success(new List<OpShopView>(), 0);
            }
            var shops = await _shopBll.GetShopPage(shopRequest);
            if (shops == null || shops.Item2 == 0)
            {
                return ResponsePagingBase.Success(new List<OpShopView>(), 0);
            }
            var areaIds = new List<long>();
            foreach (var g in shops.Item1)
            {
                areaIds.Add(g.Province.Value);
                areaIds.Add(g.City.Value);
                areaIds.Add(g.District.Value);
                areaIds.Add(g.Street.Value);
            }
            var areas = await _areaBll.GetArea(areaIds.Distinct().ToList());
            return ResponsePagingBase.Success(shops.Item1.Select(p => new OpShopView()
            {
                ShopName = p.ShopName,
                ShopTelphone = p.ShopTelphone,
                ShopAddress = p.ShopAddress,
                ShopLinkMan = p.ShopLinkMan,
                Province = GetAreaName(p.Province.Value, areas),
                City = GetAreaName(p.City.Value, areas),
                District = GetAreaName(p.District.Value, areas),
                Stree = GetAreaName(p.Street.Value, areas)
            }), shops.Item2);
        }

        /// <summary>
        /// 获取地区名称
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="areas"></param>
        /// <returns></returns>
        private string GetAreaName(long areaId, List<Area> areas)
        {
            return areas.FirstOrDefault(p => p.AreaId == areaId)?.AreaName;
        }
    }
}
