using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Web.User.Request;
using TTY.GMP.IBusiness;

namespace TTY.GMP.WebApi.Controllers.User
{
    /// <summary>
    /// 改变用户状态
    /// </summary>
    public class ChangeStatusFlagAction
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        private readonly ISysUserBLL _sysUserBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserBll"></param>
        public ChangeStatusFlagAction(ISysUserBLL sysUserBll)
        {
            this._sysUserBll = sysUserBll;
        }

        /// <summary>
        /// 改变用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(ChangeStatusFlagRequest request)
        {
            var user = await _sysUserBll.GetSysUser(request.UserId);
            if (user == null)
            {
                return new ResponseBase().GetResponseError(StatusCode.User40001, "用户不存在");
            }
            await _sysUserBll.SetUserStatusFlag(request.UserId, request.NewStatusFlag);
            return ResponseBase.Success();
        }
    }
}
