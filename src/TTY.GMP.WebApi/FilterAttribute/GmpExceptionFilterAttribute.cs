using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.LOG;

namespace TTY.GMP.WebApi.FilterAttribute
{
    /// <summary>
    /// 全局异常过滤器
    /// </summary>
    public class GmpExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 全局异常处理
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext context)
        {
            Log.Error("全局捕获的异常", context.Exception, this.GetType());
            base.OnException(context);
        }
    }
}
