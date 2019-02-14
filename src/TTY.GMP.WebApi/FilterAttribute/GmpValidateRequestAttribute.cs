using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.WebApi.Extensions;

namespace TTY.GMP.WebApi.FilterAttribute
{
    /// <summary>
    /// 验证请求参数,并赋值登录信息
    /// </summary>
    public class GmpValidateRequestAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 方法执行之前执行,验证请求的数据合法性,验证完成后赋值用户登录信息
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Count == 1 && context.ActionArguments.First().Value is RequestBase)
            {
                var request = context.ActionArguments.First().Value as RequestBase;
                if (request == null || !request.Validate())
                {
                    context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    context.Result = new JsonResult(new ResponseBase().GetResponseBadRequest());
                    return;
                }
                request.LoginUserId = context.HttpContext.Request.GetUserId();
            }
        }
    }
}
