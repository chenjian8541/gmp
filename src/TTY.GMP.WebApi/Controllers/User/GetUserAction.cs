using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Web.User.Request;
using TTY.GMP.Entity.Web.User.Response;
using TTY.GMP.IBusiness;

namespace TTY.GMP.WebApi.Controllers.User
{
    /// <summary>
    /// 通过ID获取用户信息
    /// </summary>
    public class GetUserAction
    {
        /// <summary>
        /// 用户
        /// </summary>
        private readonly ISysUserBLL _sysUserBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserBll"></param>
        public GetUserAction(ISysUserBLL sysUserBll)
        {
            this._sysUserBll = sysUserBll;
        }

        /// <summary>
        /// 通过ID获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(GetUserRequest request)
        {
            var user = await _sysUserBll.GetSysUser(request.UserId);
            if (user == null)
            {
                return new ResponseBase().GetResponseError(StatusCode.User40001, "用户不存在");
            }
            return ResponseBase.Success(new UserView(user.UserId, user.UserRoleId, user.NickName, user.Account, user.StatusFlag));
        }
    }
}
