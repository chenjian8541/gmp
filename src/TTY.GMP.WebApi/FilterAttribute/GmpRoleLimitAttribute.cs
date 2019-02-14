using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using TTY.GMP.WebApi.Common;
using TTY.GMP.Authority;
using TTY.GMP.WebApi.Core;
using Microsoft.AspNetCore.Mvc;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.WebApi.FilterAttribute
{
    /// <summary>
    /// 角色权限限制
    /// </summary>
    public class GmpRoleLimitAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 执行方法之前，验证用户角色权限
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDescriptor != null)
            {
                var authorityConfig = MenuLib.MenuConfigs.FirstOrDefault(p => p.Controller == actionDescriptor.ControllerName && p.Action == actionDescriptor.ActionName);
                if (authorityConfig != null)
                {
                    var isCanVisit = new AuthorityCore(AppTicket.GetAppTicket(context.HttpContext).WeightSum).Validation(authorityConfig.Id);
                    if (!isCanVisit)
                    {
                        context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                        context.Result = new JsonResult(new ResponseBase().GetResponseForbidden());
                    }
                }
            }
        }
    }
}
