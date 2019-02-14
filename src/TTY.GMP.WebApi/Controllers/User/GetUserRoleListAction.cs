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
    /// 获取角色列表
    /// </summary>
    public class GetUserRoleListAction
    {
        /// <summary>
        /// 用户角色业务访问
        /// </summary>
        private readonly ISysUserRoleBLL _sysUserRoleBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserRoleBll"></param>
        public GetUserRoleListAction(ISysUserRoleBLL sysUserRoleBll)
        {
            this._sysUserRoleBll = sysUserRoleBll;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(GetUserRoleListRequest request)
        {
            var roles = await _sysUserRoleBll.GetUserRoleList(request);
            var rowNumber = 1;
            var roleViews = roles.Select(p => new UserRoleView(rowNumber++, p.UserRoleId, p.Name));
            return ResponseBase.Success(roleViews);
        }
    }
}
