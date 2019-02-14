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
    /// 用户修改密码
    /// </summary>
    public class ChangePasswordAction
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        private readonly ISysUserBLL _sysUserBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserBll"></param>
        public ChangePasswordAction(ISysUserBLL sysUserBll)
        {
            this._sysUserBll = sysUserBll;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(ChangePasswordRequest request)
        {
            var user = await _sysUserBll.GetSysUser(request.LoginUserId);
            var oldPwd = CryptogramHelper.Encrypt3DES(request.OldPassword);
            if (oldPwd != user.Pwd)
            {
                return new ResponseBase().GetResponseError(StatusCode.User40004, "旧密码不正确");
            }
            var newPwd = CryptogramHelper.Encrypt3DES(request.NewPassword);
            await _sysUserBll.ChangePassword(request.LoginUserId, newPwd);
            return ResponseBase.Success();
        }
    }
}
