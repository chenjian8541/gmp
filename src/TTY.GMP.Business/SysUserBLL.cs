using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.CacheBucket;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Web.User.Request;
using TTY.GMP.IBusiness;
using TTY.GMP.IDataAccess;
using TTY.GMP.Utility;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 用户业务访问
    /// </summary>
    public class SysUserBLL : ISysUserBLL
    {
        /// <summary>
        /// 用户数据访问
        /// </summary>
        private readonly ISysUserDAL _sysUserDal;

        /// <summary>
        /// 用户登录失败数据访问
        /// </summary>
        private readonly IUserLoginFailedRecordDAL _userLoginFailedRecordDal;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserDal"></param>
        /// <param name="userLoginFailedRecordDal"></param>
        public SysUserBLL(ISysUserDAL sysUserDal, IUserLoginFailedRecordDAL userLoginFailedRecordDal)
        {
            this._sysUserDal = sysUserDal;
            this._userLoginFailedRecordDal = userLoginFailedRecordDal;
        }

        /// <summary>
        /// 通过ID获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<SysUser> GetSysUser(long userId)
        {
            return await _sysUserDal.GetSysUser(userId);
        }

        /// <summary>
        /// 通过帐号密码获取用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<SysUser> GetSysUser(string account, string pwd)
        {
            return await _sysUserDal.GetSysUser(account, pwd);
        }

        /// <summary>
        /// 更新用户最后登录时间
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newDateTime"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserLastLoginTime(long userId, DateTime newDateTime)
        {
            return await _sysUserDal.UpdateUserLastLoginTime(userId, newDateTime);
        }

        /// <summary>
        /// 判断某角色下是否存在用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<bool> ExistSysUserByRoleId(long roleId)
        {
            return await _sysUserDal.ExistSysUserByRoleId(roleId);
        }

        /// <summary>
        /// 判断帐号是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> ExistSysUserByAccount(string account, long userId = 0)
        {
            return await _sysUserDal.ExistSysUserByAccount(account, userId);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public async Task<bool> AddUser(SysUser sysUser)
        {
            sysUser.UserId = PrimaryKeyHelper.Instance.CreateID();
            return await _sysUserDal.AddUser(sysUser);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(SysUser sysUser)
        {
            return await _sysUserDal.UpdateUser(sysUser);
        }

        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="statusFlag"></param>
        /// <returns></returns>
        public async Task<bool> SetUserStatusFlag(long userId, int statusFlag)
        {
            return await _sysUserDal.SetUserStatusFlag(userId, statusFlag);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public async Task<bool> ChangePassword(long userId, string newPwd)
        {
            return await _sysUserDal.ChangePassword(userId, newPwd);
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
            return await _sysUserDal.SetDataLimit(userId, dataLimitType, dataLimitArea, dataLimitShop);
        }

        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<List<SysUser>, int>> GetUserPage(GetUserPageRequest request)
        {
            return await _sysUserDal.GetUserPage(request);
        }

        /// <summary>
        /// 通过Account获取登录失败次数
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<UserLoginFailedBucket> GetUserLoginFailedRecord(string account)
        {
            return await _userLoginFailedRecordDal.GetUserLoginFailedRecord(account);
        }

        /// <summary>
        /// 增加Account登录失败次数
        /// </summary>
        /// <param name="account"></param>
        /// <param name="loginFailedMaxCount"></param>
        /// <param name="loginFailedTimeOut"></param>
        /// <returns></returns>
        public async Task<UserLoginFailedBucket> AddUserLoginFailedRecord(string account, int loginFailedMaxCount, int loginFailedTimeOut)
        {
            return await _userLoginFailedRecordDal.AddUserLoginFailedRecord(account, loginFailedMaxCount, loginFailedTimeOut);
        }

        /// <summary>
        /// 移除Account登录失败记录
        /// </summary>
        /// <param name="account"></param>
        public async Task RemoveUserLoginFailedRecord(string account)
        {
            await _userLoginFailedRecordDal.RemoveUserLoginFailedRecord(account);
        }
    }
}
