using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.CacheBucket;
using TTY.GMP.Entity.Database;
using TTY.GMP.ICache;
using TTY.GMP.IDataAccess;
using System.Linq;
using System.Threading.Tasks;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 用户角色数据访问
    /// </summary>
    public class SysUserRoleDAL : BaseCacheDAL<SysUserRoleBucket>, ISysUserRoleDAL, IDbContext<GmpDbContext>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheProvider"></param>
        public SysUserRoleDAL(ICacheProvider cacheProvider) : base(cacheProvider)
        {
        }

        /// <summary>
        /// 获取数据访问上下文
        /// </summary>
        /// <returns></returns>
        public GmpDbContext GetDbContext()
        {
            return new GmpDbContext();
        }

        /// <summary>
        /// 从数据库中
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        protected override async Task<SysUserRoleBucket> GetDb(params object[] keys)
        {
            var roles = await this.FindList<GmpDbContext, SysUserRole>();
            if (roles.Count > 0)
            {
                return new SysUserRoleBucket() { SysUserRoles = roles };
            }
            return null;
        }

        /// <summary>
        /// 获取所有的用户角色
        /// </summary>
        /// <returns></returns>
        public async Task<List<SysUserRole>> GetSysUserRole()
        {
            var sysUserRoleBuckets = await base.GetCache();
            if (sysUserRoleBuckets != null)
            {
                return sysUserRoleBuckets.SysUserRoles;
            }
            return null;
        }

        /// <summary>
        /// 通过用户角色ID获取角色信息
        /// </summary>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        public async Task<SysUserRole> GetSysUserRole(long userRoleId)
        {
            var roles = await GetSysUserRole();
            return roles.FirstOrDefault(p => p.UserRoleId == userRoleId);
        }

        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="sysUserRole"></param>
        /// <returns></returns>
        public async Task<bool> AddUserRole(SysUserRole sysUserRole)
        {
            return await this.Insert(sysUserRole, async () => { await UpdateCache(); });
        }

        /// <summary>
        /// 编辑用户角色
        /// </summary>
        /// <param name="sysUserRole"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserRole(SysUserRole sysUserRole)
        {
            return await this.Update(sysUserRole, async () => { await UpdateCache(); });
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> DelUserRole(SysUserRole role)
        {
            return await this.Delete(role, async () => { await UpdateCache(); });
        }
    }
}
