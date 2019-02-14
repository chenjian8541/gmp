using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 用户角色数据访问
    /// </summary>
    public interface ISysUserRoleDAL
    {
        /// <summary>
        /// 获取所有的用户角色
        /// </summary>
        /// <returns></returns>
        Task<List<SysUserRole>> GetSysUserRole();

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
        /// 删除角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<bool> DelUserRole(SysUserRole role);
    }
}
