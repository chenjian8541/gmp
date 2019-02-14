using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.LOG;

namespace TTY.GMP.WebApi.FilterAttribute
{
    /// <summary>
    /// 统计执行日志
    /// </summary>
    public class GmpHandleLogAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 执行前时间
        /// </summary>
        private DateTime _startTime;

        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _startTime = DateTime.Now;
        }

        /// <summary>
        /// 返回结果后
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDescriptor != null)
            {
                Log.Info(string.Format("控制器{0}动作{1}：执行时间{2}毫秒", actionDescriptor.ControllerName, actionDescriptor.ActionName,
                    DateTime.Now.Subtract(_startTime).TotalMilliseconds.ToString()), this.GetType());

            }
        }
    }
}
