using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Web.User.Request;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;
using System.Linq;
using TTY.GMP.Utility;
using TTY.GMP.Entity.Common;
using System.Threading.Tasks;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 用户角色数据访问
    /// </summary>
    public class SysUserRoleBLL : ISysUserRoleBLL
    {
        /// <summary>
        /// 用户角色数据访问
        /// </summary>
        private readonly ISysUserRoleDAL _sysUserRoleDal;

        /// <summary>
        /// 用户业务访问
        /// </summary>
        private readonly ISysUserBLL _sysUserBll;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserRoleDal"></param>
        /// <param name="sysUserBll"></param>
        public SysUserRoleBLL(ISysUserRoleDAL sysUserRoleDal, ISysUserBLL sysUserBll)
        {
            this._sysUserRoleDal = sysUserRoleDal;
            this._sysUserBll = sysUserBll;
        }

        /// <summary>
        /// 通过条件获取用户角色信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<SysUserRole>> GetUserRoleList(GetUserRoleListRequest request)
        {
            var roles = await _sysUserRoleDal.GetSysUserRole();
            if (string.IsNullOrEmpty(request.RoleName))
            {
                return roles;
            }
            return roles.Where(p => p.Name.Contains(request.RoleName)).ToList();
        }

        /// <summary>
        /// 通过用户角色ID获取角色信息
        /// </summary>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        public async Task<SysUserRole> GetSysUserRole(long userRoleId)
        {
            return await _sysUserRoleDal.GetSysUserRole(userRoleId);
        }

        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="sysUserRole"></param>
        /// <returns></returns>
        public async Task<bool> AddUserRole(SysUserRole sysUserRole)
        {
            sysUserRole.UserRoleId = PrimaryKeyHelper.Instance.CreateID();
            return await _sysUserRoleDal.AddUserRole(sysUserRole);
        }

        /// <summary>
        /// 编辑用户角色
        /// </summary>
        /// <param name="sysUserRole"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserRole(SysUserRole sysUserRole)
        {
            return await _sysUserRoleDal.UpdateUserRole(sysUserRole);
        }

        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<Tuple<string, string>> DelSysUserRole(long roleId)
        {
            var role = await _sysUserRoleDal.GetSysUserRole(roleId);
            if (role == null)
            {
                return Tuple.Create(StatusCode.UserRole30001, "角色不存在");
            }
            if (await _sysUserBll.ExistSysUserByRoleId(roleId))
            {
                return Tuple.Create(StatusCode.UserRole30002, "角色下存在用户");
            }
            await _sysUserRoleDal.DelUserRole(role);
            return Tuple.Create(string.Empty, string.Empty);
        }
    }
}
