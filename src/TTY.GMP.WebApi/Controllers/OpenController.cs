using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTY.Api.Throttle;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Open.Request;
using TTY.GMP.Entity.View;
using TTY.GMP.Entity.Web.Open.Request;
using TTY.GMP.IBusiness;
using TTY.GMP.LOG;
using TTY.GMP.WebApi.Attribute;
using TTY.GMP.WebApi.Common;
using TTY.GMP.WebApi.Controllers.Open;

namespace TTY.GMP.WebApi.Controllers
{
    /// <summary>
    /// 公开服务
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class OpenController : Controller
    {
        /// <summary>
        /// 门店销售单排名业务
        /// </summary>
        private readonly IShopRetailRankBLL _shopRetailRankBll;

        /// <summary>
        /// 门店销售单排名限制名单
        /// </summary>
        private readonly IShopRetailRankLimitBLL _shopRetailRankLimitBll;

        /// <summary>
        /// 用户登录
        /// </summary>
        private readonly ISysUserBLL _sysUserBll;

        /// <summary>
        /// 用户角色业务访问
        /// </summary>
        private readonly ISysUserRoleBLL _sysUserRoleBll;

        /// <summary>
        /// 用户日志操作
        /// </summary>
        private readonly ISysUserLogBLL _sysUserLogBll;

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
        /// 店面业务逻辑
        /// </summary>
        private readonly IShopBLL _shopBll;

        /// <summary>
        /// 供应商业务
        /// </summary>
        private readonly IBaseSupplierBLL _baseSupplierBll;

        /// <summary>
        /// 客户信息
        /// </summary>
        private readonly IRetailCustomerBLL _retailCustomerBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopRetailRankBll"></param>
        /// <param name="shopRetailRankLimitBll"></param>
        /// <param name="sysUserBll"></param>
        /// <param name="sysUserRoleBll"></param>
        /// <param name="sysUserLogBll"></param>
        /// <param name="areaBll"></param>
        /// <param name="goodsBll"></param>
        /// <param name="openDataBll"></param>
        /// <param name="shopBll"></param>
        /// <param name="baseSupplierBll"></param>
        /// <param name="retailCustomerBll"></param>
        public OpenController(IShopRetailRankBLL shopRetailRankBll, IShopRetailRankLimitBLL shopRetailRankLimitBll,
            ISysUserBLL sysUserBll, ISysUserRoleBLL sysUserRoleBll, ISysUserLogBLL sysUserLogBll,
            IOpenDataBLL openDataBll, IGoodsBLL goodsBll, IAreaBLL areaBll, IShopBLL shopBll,
            IBaseSupplierBLL baseSupplierBll, IRetailCustomerBLL retailCustomerBll)
        {
            this._shopRetailRankBll = shopRetailRankBll;
            this._shopRetailRankLimitBll = shopRetailRankLimitBll;
            this._sysUserBll = sysUserBll;
            this._sysUserRoleBll = sysUserRoleBll;
            this._sysUserLogBll = sysUserLogBll;
            this._openDataBll = openDataBll;
            this._goodsBll = goodsBll;
            this._areaBll = areaBll;
            this._shopBll = shopBll;
            this._baseSupplierBll = baseSupplierBll;
            this._retailCustomerBll = retailCustomerBll;
        }

        /// <summary>
        /// 获取门店销售单排名信息(成长之星)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseBase> GetShopRetailRankAboutGrowth([FromBody]GetShopRetailRankAboutGrowthRequest request)
        {
            try
            {
                var action = new GetShopRetailRankAboutGrowthAction(_shopRetailRankBll, _shopRetailRankLimitBll);
                return await action.ProcessAction(request.ShopId);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 获取门店销售单排名信息(订单王)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseBase> GetShopRetailRankAboutKing([FromBody]GetShopRetailRankAboutKingRequest request)
        {
            try
            {
                var action = new GetShopRetailRankAboutKingAction(_shopRetailRankBll);
                return await action.ProcessAction(request.ShopId);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 获取接口访问AccessToken
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [RateValve(Policy = Policy.Ip, Limit = 1, Duration = 2)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseBase> GetAccessToken([FromBody]GetAccessTokenRequest request)
        {
            try
            {
                var action = new GetAccessTokenAction(_sysUserBll, _sysUserRoleBll, _sysUserLogBll);
                return await action.ProcessAction(this.HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return ResponseBase.CodeError();
            }
        }

        /// <summary>
        /// 分页获取商品及库存信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [RateValve(Policy = Policy.Ip, Limit = 1, Duration = 2)]
        [UserBehavior(UserLogEnum.GetGoodsPaging, "[开放接口]获取商品和库存信息")]
        [HttpPost]
        public async Task<ResponsePagingBase> GetGoodsPaging([FromBody] GetGoodsPagingRequest request)
        {
            try
            {
                var action = new GetGoodsPagingAction(_openDataBll, _goodsBll, _areaBll);
                return await action.ProcessAction(this.HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }

        /// <summary>
        /// 分页获取门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [RateValve(Policy = Policy.Ip, Limit = 1, Duration = 2)]
        [UserBehavior(UserLogEnum.GetShopPaging, "[开放接口]获取门店信息")]
        [HttpPost]
        public async Task<ResponsePagingBase> GetShopPaging([FromBody] GetShopPagingReqest request)
        {
            try
            {
                var action = new GetShopPagingAction(_shopBll, _areaBll);
                return await action.ProcessAction(this.HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }

        /// <summary>
        /// [开放接口]获取采购信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [RateValve(Policy = Policy.Ip, Limit = 1, Duration = 2)]
        [UserBehavior(UserLogEnum.GetPurchasePaging, "[开放接口]获取采购信息")]
        [HttpPost]
        public async Task<ResponsePagingBase> GetPurchasePaging([FromBody] GetPurchasePagingRequest request)
        {
            try
            {
                var action = new GetPurchasePagingAction(_openDataBll, _goodsBll, _areaBll, _baseSupplierBll);
                return await action.ProcessAction(this.HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }

        /// <summary>
        /// [开放接口]获取采购退货信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [RateValve(Policy = Policy.Ip, Limit = 1, Duration = 2)]
        [UserBehavior(UserLogEnum.GetPurchaseBackPaging, "[开放接口]获取采购退货信息")]
        [HttpPost]
        public async Task<ResponsePagingBase> GetPurchaseBackPaging([FromBody] GetPurchaseBackPagingRequest request)
        {
            try
            {
                var action = new GetPurchaseBackPagingAction(_openDataBll, _goodsBll, _areaBll, _baseSupplierBll);
                return await action.ProcessAction(this.HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }

        /// <summary>
        /// [开放接口]获取销售商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [RateValve(Policy = Policy.Ip, Limit = 1, Duration = 2)]
        [UserBehavior(UserLogEnum.GetSaleGoodsPaging, "[开放接口]获取销售商品信息")]
        [HttpPost]
        public async Task<ResponsePagingBase> GetSaleGoodsPaging([FromBody] GetSaleGoodsPagingRequest request)
        {
            try
            {
                var action = new GetSaleGoodsPagingAction(_openDataBll, _goodsBll, _areaBll, _retailCustomerBll);
                return await action.ProcessAction(this.HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }

        /// <summary>
        /// [开放接口]获取销售退货信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [RateValve(Policy = Policy.Ip, Limit = 1, Duration = 2)]
        [UserBehavior(UserLogEnum.GetSaleGoodsPaging, "[开放接口]获取销售退货信息")]
        [HttpPost]
        public async Task<ResponsePagingBase> GetSaleGoodsBackPaging([FromBody] GetSaleGoodsBackPagingRequest request)
        {
            try
            {
                var action = new GetSaleGoodsBackPagingAction(_openDataBll, _goodsBll, _areaBll, _retailCustomerBll);
                return await action.ProcessAction(this.HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }
    }
}