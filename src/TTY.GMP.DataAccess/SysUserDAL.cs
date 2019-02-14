using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.DataCore;
using TTY.GMP.Entity.Database;
using TTY.GMP.ICache;
using TTY.GMP.IDataAccess;
using TTY.GMP.Utility;
using System.Linq;
using TTY.GMP.Entity.Enum;
using TTY.GMP.Entity.Web.User.Request;
using System.Threading.Tasks;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 系统用户数据访问
    /// </summary>
    public class SysUserDAL : BaseCacheDAL<SysUser>, ISysUserDAL, IDbContext<GmpDbContext>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheProvider"></param>
        public SysUserDAL(ICacheProvider cacheProvider) : base(cacheProvider)
        { }

        /// <summary>
        /// 获取数据访问上下文
        /// </summary>
        /// <returns></returns>
        public GmpDbContext GetDbContext()
        {
            return new GmpDbContext();
        }

        /// <summary>
        /// 从数据库中获取需要缓存的信息
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        protected override async Task<SysUser> GetDb(params object[] keys)
        {
            return await this.Find<GmpDbContext, SysUser>(p => p.UserId == keys[0].ToLong() && p.DataFlag == (int)DataFlagEnum.Normal);
        }

        /// <summary>
        /// 通过ID获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<SysUser> GetSysUser(long userId)
        {
            return await base.GetCache(userId);
        }

        /// <summary>
        /// 通过帐号密码获取用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<SysUser> GetSysUser(string account, string pwd)
        {
            return await this.Find<GmpDbContext, SysUser>(p => p.Account.Equals(account) && p.Pwd.Equals(pwd) && p.DataFlag == (int)DataFlagEnum.Normal);
        }

        /// <summary>
        /// 更新用户最后登录时间
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newDateTime"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserLastLoginTime(long userId, DateTime newDateTime)
        {
            var user = await GetSysUser(userId);
            if (user == null)
            {
                return false;
            }
            user.LastLoginTime = newDateTime;
            return await this.Update(user, async () => { await UpdateCache(userId); });
        }

        /// <summary>
        /// 判断某角色下是否存在用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<bool> ExistSysUserByRoleId(long roleId)
        {
            return await this.Find<GmpDbContext, SysUser>(p => p.UserRoleId == roleId) != null;
        }

        /// <summary>
        /// 判断帐号是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> ExistSysUserByAccount(string account, long userId = 0)
        {
            return await this.Find<GmpDbContext, SysUser>(p => p.Account == account && p.UserId != userId) != null;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public async Task<bool> AddUser(SysUser sysUser)
        {
            return await this.Insert(sysUser, async () => { await UpdateCache(sysUser.UserId); });
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(SysUser sysUser)
        {
            return await this.Update(sysUser, async () => { await UpdateCache(sysUser.UserId); });
        }

        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="statusFlag"></param>
        /// <returns></returns>
        public async Task<bool> SetUserStatusFlag(long userId, int statusFlag)
        {
            var user = await GetSysUser(userId);
            if (user == null)
            {
                return false;
            }
            if (user.StatusFlag == statusFlag)
            {
                return true;
            }
            user.StatusFlag = statusFlag;
            return await this.Update(user, async () => { await UpdateCache(userId); });
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public async Task<bool> ChangePassword(long userId, string newPwd)
        {
            var user = await GetSysUser(userId);
            if (user == null)
            {
                return false;
            }
            user.Pwd = newPwd;
            return await this.Update(user, async () => { await UpdateCache(userId); });
        }

        /// <summary>
        /// 设置用户数据权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataLimitType"></param>
        /// <param name="dataLimitArea"></param>
        /// <param name="dataLimitShop"></param>
        /// <returns></returns>
        public async Task<bool> SetDataLimit(long userId, int dataLimitType, string dataLimitArea, string dataLimitShop)
        {
            var user = await GetSysUser(userId);
            if (user == null)
            {
                return false;
            }
            user.DataLimitType = dataLimitType;
            user.DataLimitArea = dataLimitArea;
            user.DataLimitShop = dataLimitShop;
            return await this.Update(user, async () => { await UpdateCache(userId); });
        }

        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<List<SysUser>, int>> GetUserPage(GetUserPageRequest request)
        {
            var expression = LinqExtensions.True<SysUser>();
            if (!string.IsNullOrEmpty(request.NickName))
            {
                expression = expression.And(p => p.NickName.Contains(request.NickName));
            }
            if (!string.IsNullOrEmpty(request.Account))
            {
                expression = expression.And(p => p.Account.Contains(request.Account));
            }
            return await this.FindPage(request.PageCurrent, request.PageSize, expression, p => p.UserId);
        }
    }
}
