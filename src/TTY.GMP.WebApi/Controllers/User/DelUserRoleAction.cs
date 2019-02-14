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
    /// 删除角色
    /// </summary>
    public class DelUserRoleAction
    {
        /// <summary>
        /// 用户角色业务访问
        /// </summary>
        private readonly ISysUserRoleBLL _sysUserRoleBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserRoleBll"></param>
        public DelUserRoleAction(ISysUserRoleBLL sysUserRoleBll)
        {
            this._sysUserRoleBll = sysUserRoleBll;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(DelUserRoleRequest request)
        {
            var result = await _sysUserRoleBll.DelSysUserRole(request.UserRoleId);
            if (result.Item1 != string.Empty)
            {
                return new ResponseBase().GetResponseError(result.Item1, result.Item2);
            }
            return ResponseBase.Success();
        }
    }
}
