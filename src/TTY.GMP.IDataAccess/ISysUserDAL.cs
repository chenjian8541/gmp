using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Web.User.Request;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 系统用户数据访问
    /// </summary>
    public interface ISysUserDAL
    {
        /// <summary>
        /// 通过ID获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<SysUser> GetSysUser(long userId);

        /// <summary>
        /// 通过帐号密码获取用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        Task<SysUser> GetSysUser(string account, string pwd);

        /// <summary>
        /// 更新用户最后登录时间
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newDateTime"></param>
        /// <returns></returns>
        Task<bool> UpdateUserLastLoginTime(long userId, DateTime newDateTime);

        /// <summary>
        /// 判断某角色下是否存在用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<bool> ExistSysUserByRoleId(long roleId);

        /// <summary>
        /// 判断帐号是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> ExistSysUserByAccount(string account, long userId = 0);

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        Task<bool> AddUser(SysUser sysUser);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        Task<bool> UpdateUser(SysUser sysUser);

        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="statusFlag"></param>
        /// <returns></returns>
        Task<bool> SetUserStatusFlag(long userId, int statusFlag);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        Task<bool> ChangePassword(long userId, string newPwd);

        /// <summary>
        /// 设置用户数据权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataLimitType"></param>
        /// <param name="dataLimitArea"></param>
        /// <param name="dataLimitShop"></param>
        /// <returns></returns>
        Task<bool> SetDataLimit(long userId, int dataLimitType, string dataLimitArea, string dataLimitShop);

        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<List<SysUser>, int>> GetUserPage(GetUserPageRequest request);
    }
}
