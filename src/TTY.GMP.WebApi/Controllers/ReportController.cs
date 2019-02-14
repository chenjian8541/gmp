using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Web.Report.Request;
using TTY.GMP.IBusiness;
using TTY.GMP.LOG;
using TTY.GMP.WebApi.Attribute;
using TTY.GMP.WebApi.Controllers.Report;

namespace TTY.GMP.WebApi.Controllers
{
    /// <summary>
    /// 报表数据查询控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ReportController : Controller
    {
        /// <summary>
        /// 地区业务访问
        /// </summary>
        private readonly IAreaBLL _areaBll;

        /// <summary>
        /// 报表业务访问
        /// </summary>
        private readonly IReportBLL _reportBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="areaBll"></param>
        /// <param name="reportBll"></param>
        public ReportController(IAreaBLL areaBll, IReportBLL reportBll)
        {
            this._areaBll = areaBll;
            this._reportBll = reportBll;
        }

        /// <summary>
        /// 获取区域农药销售统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.GetStatisticsRetail, "区域农药销售统计")]
        public async Task<ResponseBase> GetStatisticsRetail([FromBody]GetStatisticsRetailRequest request)
        {
            try
            {
                var action = new GetStatisticsRetailAction(_areaBll, _reportBll);
                return await action.ProcessAction(this.HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }

        /// <summary>
        /// 获取区域农药采购统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.GetStatisticsPurchase, "区域农药采购统计")]
        public async Task<ResponseBase> GetStatisticsPurchase([FromBody]GetStatisticsPurchaseRequest request)
        {
            try
            {
                var action = new GetStatisticsPurchaseAction(_areaBll, _reportBll);
                return await action.ProcessAction(this.HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }

        /// <summary>
        /// 获取区域农药库存统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.GetStatisticsStock, "区域农药库存统计")]
        public async Task<ResponseBase> GetStatisticsStock([FromBody]GetStatisticsStockRequest request)
        {
            try
            {
                var action = new GetStatisticsStockAction(_areaBll, _reportBll);
                return await action.ProcessAction(this.HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }

        /// <summary>
        /// 区域门店销售看板
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.GetStatisticsRetailShopPage, "区域门店销售看板")]
        public async Task<ResponsePagingBase> GetStatisticsRetailShopPage([FromBody]GetStatisticsRetailShopPageRequest request)
        {
            try
            {
                var action = new GetStatisticsRetailShopPageAction(_areaBll, _reportBll);
                return await action.ProcessAction(this.HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }

        /// <summary>
        /// 区域门店采购看板
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.GetStatisticsPurchaseShopPage, "区域门店采购看板")]
        public async Task<ResponsePagingBase> GetStatisticsPurchaseShopPage([FromBody]GetStatisticsPurchaseShopPageRequest request)
        {
            try
            {
                var action = new GetStatisticsPurchaseShopPageAction(_areaBll, _reportBll);
                return await action.ProcessAction(this.HttpContext, request);
            }
            catch (Exception ex)
            {
                Log.Error(request, ex, this.GetType());
                return new ResponsePagingBase().GetResponseCodeError();
            }
        }

        /// <summary>
        /// 区域门店库存看板
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserBehavior(UserLogEnum.GetStatisticsStockShopPage, "区域门店库存看板")]
        public async Task<ResponsePagingBase> GetStatisticsStockShopPage([FromBody]GetStatisticsStockShopPageRequest request)
        {
            try
            {
                var action = new GetStatisticsStockShopPageAction(_areaBll, _reportBll);
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