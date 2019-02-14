using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.DataAccess.Common;
using TTY.GMP.Entity.CacheBucket;
using TTY.GMP.ICache;
using TTY.GMP.IDataAccess;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 用户登录失败的次数
    /// </summary>
    public class UserLoginFailedRecordDAL : IUserLoginFailedRecordDAL
    {
        /// <summary>
        /// 缓存访问提供器
        /// </summary>
        private ICacheProvider _cacheProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheProvider"></param>
        public UserLoginFailedRecordDAL(ICacheProvider cacheProvider)
        {
            this._cacheProvider = cacheProvider;
        }

        /// <summary>
        /// 通过Account获取登录失败次数
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<UserLoginFailedBucket> GetUserLoginFailedRecord(string account)
        {
            return await _cacheProvider.Get<UserLoginFailedBucket>(new UserLoginFailedBucket().GetKeyFormat(account));
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
            var key = new UserLoginFailedBucket().GetKeyFormat(account);
            var record = await _cacheProvider.Get<UserLoginFailedBucket>(key);
            if (record == null)
            {
                record = new UserLoginFailedBucket()
                {
                    Account = account,
                    FailedCount = 1
                };
            }
            else
            {
                if (record.FailedCount < loginFailedMaxCount)
                {
                    record.FailedCount++;
                }
                else
                {
                    record.ExpireAtTime = DateTime.Now.AddMinutes(loginFailedTimeOut);
                }
            }
            await _cacheProvider.Set(key, record, CacheConfig.TimeOutLoginFailed);
            return record;
        }

        /// <summary>
        /// 移除Account登录失败记录
        /// </summary>
        /// <param name="account"></param>
        public async Task RemoveUserLoginFailedRecord(string account)
        {
            var key = new UserLoginFailedBucket().GetKeyFormat(account);
            await _cacheProvider.Remove(key);
        }
    }
}
