using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Web.Goods.Request;
using TTY.GMP.IBusiness;
using TTY.GMP.LOG;
using TTY.GMP.WebApi.Attribute;
using TTY.GMP.WebApi.Controllers.Goods;

namespace TTY.GMP.WebApi.Controllers
{
    /// <summary>
    /// 商品访问控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class GoodsController : Controller
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
        public GoodsController(IShopBLL shopBll, IGoodsBLL goodsBll)
        {
            this._shopBll = shopBll;
            this._goodsBll = goodsBll;
        }

        /// <summary>
        /// 分页获取门店销售的商品信息
        /// </summary>
        /// <returns></returns>
        /// <param name="request"></param>
        [HttpPost]
        [UserBehavior(UserLogEnum.SaleGoods, "销售商品")]
        public async Task<ResponsePagingBase> GetSaleGoods([FromBody]GetSaleGoodsPageRequest request)
        {
            try
            {
                var action = new GetSaleGoodsAction(_shopBll, _goodsBll);
                return await action.ProcessAction(request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }

        /// <summary>
        /// 分页获取商品销售记录信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.SaleGoodsLog, "销售监管")]
        public async Task<ResponsePagingBase> GetSaleGoodsLog([FromBody] GetSaleGoodsLogPageRequest request)
        {
            try
            {
                var action = new GetSaleGoodsLogAction(_goodsBll);
                return await action.ProcessAction(HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }

        /// <summary>
        /// 分页获取采购进货记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.ReceiptInLog, "采购监管")]
        public async Task<ResponsePagingBase> GetReceiptInLog([FromBody] ReceiptInLogPageRequest request)
        {
            try
            {
                var action = new GetReceiptInLogAction(_goodsBll);
                return await action.ProcessAction(HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }
    }
}
