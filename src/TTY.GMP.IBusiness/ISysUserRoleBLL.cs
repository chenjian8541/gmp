using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Web.User.Request;

namespace TTY.GMP.IBusiness
{
    /// <summary>
    /// 用户角色业务访问
    /// </summary>
    public interface ISysUserRoleBLL
    {
        /// <summary>
        /// 通过条件获取用户角色信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<SysUserRole>> GetUserRoleList(GetUserRoleListRequest request);

        /// <summary>
        /// 通过用户角色ID获取角色信息
        /// </summary>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        Task<SysUserRole> GetSysUserRole(long userRoleId);

        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="sysUserRole"></param>
        /// <returns></returns>
        Task<bool> AddUserRole(SysUserRole sysUserRole);

        /// <summary>
        /// 编辑用户角色
        /// </summary>
        /// <param name="sysUserRole"></param>
        /// <returns></returns>
        Task<bool> UpdateUserRole(SysUserRole sysUserRole);

        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<Tuple<string, string>> DelSysUserRole(long roleId);
    }
}
