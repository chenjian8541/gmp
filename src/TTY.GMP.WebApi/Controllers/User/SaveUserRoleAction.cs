using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTY.GMP.Authority;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Web.User.Request;
using TTY.GMP.IBusiness;

namespace TTY.GMP.WebApi.Controllers.User
{
    /// <summary>
    /// 保存用户角色信息
    /// </summary>
    public class SaveUserRoleAction
    {
        /// <summary>
        /// 用户角色业务访问
        /// </summary>
        private readonly ISysUserRoleBLL _sysUserRoleBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserRoleBll"></param>
        public SaveUserRoleAction(ISysUserRoleBLL sysUserRoleBll)
        {
            this._sysUserRoleBll = sysUserRoleBll;
        }

        /// <summary>
        /// 保存用户角色信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseBase> ProcessAction(SaveUserRoleRequest request)
        {
            if (request.UserRoleId > 0)
            {
                return await EditUserRole(request);
            }
            return await AddUserRole(request);
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponseBase> EditUserRole(SaveUserRoleRequest request)
        {
            var role = await _sysUserRoleBll.GetSysUserRole(request.UserRoleId);
            if (role == null)
            {
                return new ResponseBase().GetResponseError(StatusCode.UserRole30001, "角色不存在");
            }
            role.Name = request.Name;
            role.AuthorityValue = GetAuthorityValue(request.Ids);
            await _sysUserRoleBll.UpdateUserRole(role);
            return ResponseBase.Success();
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ResponseBase> AddUserRole(SaveUserRoleRequest request)
        {
            var role = new SysUserRole()
            {
                Name = request.Name,
                Remark = string.Empty,
                AuthorityValue = GetAuthorityValue(request.Ids)
            };
            await _sysUserRoleBll.AddUserRole(role);
            return ResponseBase.Success();
        }

        /// <summary>
        /// 通过选择的菜单ID，计算权值
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        private string GetAuthorityValue(int[] ids)
        {
            var authorityCore = new AuthorityCore();
            var weightSum = authorityCore.AuthoritySum(ids);
            return weightSum.ToString();
        }
    }
}
