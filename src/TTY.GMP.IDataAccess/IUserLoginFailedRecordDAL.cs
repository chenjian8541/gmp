using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.CacheBucket;

namespace TTY.GMP.IDataAccess
{
    /// <summary>
    /// 用户登录失败的次数
    /// </summary>
    public interface IUserLoginFailedRecordDAL
    {
        /// <summary>
        /// 通过Account获取登录失败次数
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<UserLoginFailedBucket> GetUserLoginFailedRecord(string account);

        /// <summary>
        /// 增加Account登录失败次数
        /// </summary>
        /// <param name="account"></param>
        /// <param name="loginFailedMaxCount"></param>
        /// <param name="loginFailedTimeOut"></param>
        /// <returns></returns>
        Task<UserLoginFailedBucket> AddUserLoginFailedRecord(string account, int loginFailedMaxCount, int loginFailedTimeOut);

        /// <summary>
        /// 移除Account登录失败记录
        /// </summary>
        /// <param name="account"></param>
        Task RemoveUserLoginFailedRecord(string account);
    }
}
