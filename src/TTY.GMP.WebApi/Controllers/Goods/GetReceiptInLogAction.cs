using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.View;
using TTY.GMP.Entity.Web.Goods.Request;
using TTY.GMP.IBusiness;
using TTY.GMP.Utility;
using TTY.GMP.WebApi.Common;

namespace TTY.GMP.WebApi.Controllers.Goods
{
    /// <summary>
    /// 获取采购进货
    /// </summary>
    public class GetReceiptInLogAction
    {
        /// <summary>
        /// 商品业务逻辑
        /// </summary>
        private readonly IGoodsBLL _goodsBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="goodsBll"></param>
        public GetReceiptInLogAction(IGoodsBLL goodsBll)
        {
            this._goodsBll = goodsBll;
        }

        /// <summary>
        /// 分页获取商品销售记录信息
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponsePagingBase> ProcessAction(HttpContext httpContext, ReceiptInLogPageRequest request)
        {
            if (!HandleRequest(httpContext, request))
            {
                return ResponsePagingBase.Success(new List<ReceiptInLogView>(), 0);
            }
            var result = await _goodsBll.GetReceiptInLogPage(request);
            result.Item1.ToList().ForEach(p => p.CreateTime = p.InDate.ToDateTimeFromTimeStamp());
            return ResponsePagingBase.Success(result.Item1, result.Item2);
        }

        /// <summary>
        /// 处理请求参数
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="request"></param>
        private bool HandleRequest(HttpContext httpContext, ReceiptInLogPageRequest request)
        {
            if (request.StartTime != null)
            {
                request.StartTimeStamp = request.StartTime.Value.ToTimestamp();
            }
            if (request.EndTime != null)
            {
                request.EndTimeStamp = request.EndTime.Value.Date.AddDays(1).AddSeconds(-1).ToTimestamp();
            }
            return ComLib.HandleRequest(httpContext, request);
        }
    }
}
