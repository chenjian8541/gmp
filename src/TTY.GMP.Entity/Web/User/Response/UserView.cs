using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.User.Response
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserView
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userRoleId"></param>
        /// <param name="nickName"></param>
        /// <param name="account"></param>
        /// <param name="statusFlag"></param>
        public UserView(long userId, long userRoleId, string nickName, string account, int statusFlag)
        {
            this.UserId = userId;
            this.UserRoleId = userRoleId;
            this.NickName = nickName;
            this.Account = account;
            this.StatusFlag = statusFlag;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long UserRoleId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户帐号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户状态
        /// <see cref="TTY.GMP.Entity.Enum.UserStatusFlagEnum"/>
        /// </summary>
        public int StatusFlag { get; set; }
    }
}
