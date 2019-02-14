using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Web.Shop.Request;
using TTY.GMP.IBusiness;
using TTY.GMP.LOG;
using TTY.GMP.WebApi.Attribute;
using TTY.GMP.WebApi.Controllers.Shop;
using TTY.GMP.WebApi.Extensions;

namespace TTY.GMP.WebApi.Controllers
{
    /// <summary>
    /// 门店控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ShopController : Controller
    {
        /// <summary>
        /// 地区业务访问
        /// </summary>
        private readonly IAreaBLL _areaBll;

        /// <summary>
        /// 店面业务逻辑
        /// </summary>
        private readonly IShopBLL _shopBll;

        /// <summary>
        /// 门店统计业务
        /// </summary>
        private readonly IShopStatisticsBLL _shopStatisticsBll;

        /// <summary>
        /// 平台业务访问
        /// </summary>
        private readonly IPlatformBLL _platformBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopBll"></param>
        /// <param name="areaBll"></param>
        /// <param name="shopStatisticsBll"></param>
        /// <param name="platformBll"></param>
        public ShopController(IShopBLL shopBll, IAreaBLL areaBll, IShopStatisticsBLL shopStatisticsBll, IPlatformBLL platformBll)
        {
            this._shopBll = shopBll;
            this._areaBll = areaBll;
            this._shopStatisticsBll = shopStatisticsBll;
            this._platformBll = platformBll;
        }

        /// <summary>
        /// 通过地区获取店面信息
        /// </summary>
        /// <returns></returns>
        /// <param name="request"></param>
        [HttpPost]
        [UserBehavior(UserLogEnum.Shop, "零售商管理")]
        public async Task<ResponsePagingBase> GetShopPage([FromBody]GetShopPageRequest request)
        {
            try
            {
                var action = new GetShopByAreaAction(_shopBll, _areaBll, _platformBll);
                return await action.ProcessAction(HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }

        /// <summary>
        /// 获取门店统计信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.Map, "智控中心")]
        public async Task<ResponseBase> GetShopStatistics([FromBody] GetShopStatisticsRequest request)
        {
            try
            {
                var action = new GetShopStatisticsAction(_shopStatisticsBll);
                return await action.ProcessAction(request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }
    }
}