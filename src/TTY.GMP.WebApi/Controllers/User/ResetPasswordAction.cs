using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Web.User.Request;
using TTY.GMP.IBusiness;
using TTY.GMP.Utility;

namespace TTY.GMP.WebApi.Controllers.User
{
    /// <summary>
    /// 重置用户密码
    /// </summary>
    public class ResetPasswordAction
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        private readonly ISysUserBLL _sysUserBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserBll"></param>
        public ResetPasswordAction(ISysUserBLL sysUserBll)
        {
            this._sysUserBll = sysUserBll;
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(ResetPasswordRequest request)
        {
            var user = await _sysUserBll.GetSysUser(request.UserId);
            if (user == null)
            {
                return new ResponseBase().GetResponseError(StatusCode.User40001, "用户不存在");
            }
            var newPwd = CryptogramHelper.Encrypt3DES(request.NewPassword);
            await _sysUserBll.ChangePassword(request.UserId, newPwd);
            return ResponseBase.Success();
        }
    }
}
