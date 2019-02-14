using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Platform.Response;
using TTY.GMP.Entity.Web.Shop.Request;
using TTY.GMP.Entity.Web.Shop.Response;
using TTY.GMP.IBusiness;
using TTY.GMP.Utility;
using TTY.GMP.WebApi.Common;

namespace TTY.GMP.WebApi.Controllers.Shop
{
    /// <summary>
    /// 通过地区获取店面信息
    /// </summary>
    public class GetShopByAreaAction
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
        /// 平台业务访问
        /// </summary>
        private readonly IPlatformBLL _platformBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopBll"></param>
        /// <param name="areaBll"></param>
        /// <param name="platformBll"></param>
        public GetShopByAreaAction(IShopBLL shopBll, IAreaBLL areaBll, IPlatformBLL platformBll)
        {
            this._shopBll = shopBll;
            this._areaBll = areaBll;
            this._platformBll = platformBll;
        }

        /// <summary>
        /// 通过地区获取店面信息
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponsePagingBase> ProcessAction(HttpContext httpContext, GetShopPageRequest request)
        {
            if (request.SceneType != (int)GetShopPageSceneTypeEnum.DataLimitSetting && !ComLib.HandleRequest(httpContext, request))
            {
                return ResponsePagingBase.Success(new List<GetShopPageView>(), 0);
            }
            var shops = await _shopBll.GetShopPage(request);
            var leaderNames = GetLeaderNames(shops.Item1.ToList(), request.SceneType);
            return ResponsePagingBase.Success(shops.Item1.Select(p => new GetShopPageView()
            {
                ShopId = p.ShopId,
                ShopName = p.ShopName,
                ShopTelphone = p.ShopTelphone,
                ProvinceName = GetAreaName(p.Province),
                CityName = GetAreaName(p.City),
                DistrictName = GetAreaName(p.District),
                LicenseNum = string.Empty,
                ShopAddress = p.ShopAddress,
                LeaderName = GetLeaderName(p.OrgId, request.SceneType, leaderNames)
            }), shops.Item2);
        }

        /// <summary>
        /// 地区数据
        /// </summary>
        private List<Area> _areas;

        /// <summary>
        /// 通过地区ID获取地区名称
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        private string GetAreaName(long? areaId)
        {
            if (areaId == null)
            {
                return string.Empty;
            }
            if (_areas == null)
            {
                _areas = _areaBll.GetArea().Result;
            }
            var area = _areas.FirstOrDefault(p => p.AreaId == areaId);
            return area == null ? string.Empty : area.AreaName;
        }

        /// <summary>
        /// 获取负责人
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sceneType"></param>
        /// <param name="fullNames"></param>
        /// <returns></returns>
        private string GetLeaderName(long orgId, int sceneType, List<PfFullName> fullNames)
        {
            if (orgId == 0 || sceneType == (int)GetShopPageSceneTypeEnum.DataLimitSetting || fullNames == null || fullNames.Count == 0)
            {
                return string.Empty;
            }
            var leader = fullNames.FirstOrDefault(p => p.org_id == orgId);
            if (leader == null)
            {
                return string.Empty;
            }
            return leader.org_legal_person;
        }


        /// <summary>
        /// 获取平台负责人
        /// </summary>
        /// <param name="shops"></param>
        /// <param name="sceneType"></param>
        /// <returns></returns>
        private List<PfFullName> GetLeaderNames(List<Entity.Database.Shop> shops, int sceneType)
        {
            if (sceneType == (int)GetShopPageSceneTypeEnum.DataLimitSetting || shops == null || shops.Count == 0)
            {
                return null;
            }
            return _platformBll.GetFullNames(shops.Select(p => p.OrgId));
        }
    }
}
