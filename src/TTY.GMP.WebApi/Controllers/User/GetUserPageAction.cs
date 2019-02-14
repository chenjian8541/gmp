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
    /// 分页获取用户信息
    /// </summary>
    public class GetUserPageAction
    {
        /// <summary>
        /// 用户
        /// </summary>
        private readonly ISysUserBLL _sysUserBll;

        /// <summary>
        /// 用户角色业务访问
        /// </summary>
        private readonly ISysUserRoleBLL _sysUserRoleBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserBll"></param>
        public GetUserPageAction(ISysUserBLL sysUserBll, ISysUserRoleBLL sysUserRoleBLL)
        {
            this._sysUserBll = sysUserBll;
            this._sysUserRoleBll = sysUserRoleBLL;
        }

        /// <summary>
        /// 通过ID获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponsePagingBase> ProcessAction(GetUserPageRequest request)
        {
            var users = await _sysUserBll.GetUserPage(request);
            var userViews = users.Item1.Select(p => new UserPageView()
            {
                UserId = p.UserId,
                Account = p.Account,
                LastLoginTime = p.LastLoginTime,
                NickName = p.NickName,
                StatusFlag = p.StatusFlag,
                UserRoleId = p.UserRoleId,
                RoleName = _sysUserRoleBll.GetSysUserRole(p.UserRoleId).Result?.Name
            });
            return ResponsePagingBase.Success(userViews, users.Item2);
        }
    }
}
