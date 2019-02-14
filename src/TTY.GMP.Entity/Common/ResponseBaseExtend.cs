using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Common
{
    /// <summary>
    /// ResponseBase扩展
    /// </summary>
    public static class ResponseBaseExtend
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsResponseSuccess<T>(this T @this) where T : ResponseBase
        {
            return @this.code == StatusCode.Succeed;
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="entity">成功返回的数据信息</param>
        /// <returns></returns>
        public static T GetResponseSuccess<T>(this T @this, object entity) where T : ResponseBase
        {
            @this.code = StatusCode.Succeed;
            @this.resultdata = entity;
            return @this;
        }

        /// <summary>
        /// 无权限(禁止访问)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="returnMsg"></param>
        /// <returns></returns>
        public static T GetResponseForbidden<T>(this T @this, string returnMsg = "禁止访问") where T : ResponseBase
        {
            @this.code = StatusCode.Forbidden;
            @this.message = returnMsg;
            return @this;
        }

        /// <summary>
        /// 无权限(未登录)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="returnMsg"></param>
        /// <returns></returns>
        public static T GetResponseNotLogin<T>(this T @this, string returnMsg = "未登录") where T : ResponseBase
        {
            @this.code = StatusCode.NotLogin;
            @this.message = returnMsg;
            return @this;
        }

        /// <summary>
        /// 错误请求,数据校验错误
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="returnMsg"></param>
        /// <returns></returns>
        public static T GetResponseBadRequest<T>(this T @this, string returnMsg = "错误请求") where T : ResponseBase
        {
            @this.code = StatusCode.BadRequest;
            @this.message = returnMsg;
            return @this;
        }

        /// <summary>
        /// 返回失败
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="returnMsg"></param>
        /// <returns></returns>
        public static T GetResponseCodeError<T>(this T @this, string returnMsg = "未知错误") where T : ResponseBase
        {
            @this.code = StatusCode.CodeError;
            @this.message = returnMsg;
            return @this;
        }

        /// <summary>
        /// 返回失败
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="returnCode"></param>
        /// <param name="returnMsg"></param>
        /// <returns></returns>
        public static T GetResponseError<T>(this T @this, string returnCode, string returnMsg = "发生错误") where T : ResponseBase
        {
            @this.code = returnCode;
            @this.message = returnMsg;
            return @this;
        }
    }
}
