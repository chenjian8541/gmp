using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;
using TTY.GMP.WebApi.Attribute;
using TTY.GMP.WebApi.Extensions;

namespace TTY.GMP.WebApi.FilterAttribute
{
    /// <summary>
    /// 用户行为记录
    /// </summary>
    public class GmpUserBehaviorAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 记录用户行为日志
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDescriptor != null)
            {
                var attribute = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(UserBehaviorAttribute), false).FirstOrDefault();
                if (attribute != null)
                {
                    var userBehaviorAttribute = (UserBehaviorAttribute)attribute;
                    CustomServiceLocator.GetInstance<ISysUserLogBLL>().AddSysUserLog(new SysUserLog()
                    {
                        UserId = context.HttpContext.Request.GetUserId(),
                        IpAddress = string.Empty,
                        Ot = DateTime.Now,
                        Describe = userBehaviorAttribute.Describe,
                        Type = (int)userBehaviorAttribute.Type
                    });
                }
            }
        }
    }
}
