using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.User.Request
{
    /// <summary>
    /// 保存用户
    /// </summary>
    public class SaveUserRequest : RequestBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long UserRoleId { get; set; }

        /// <summary>
        /// 用户状态
        /// <see cref="TTY.GMP.Entity.Enum.UserStatusFlagEnum"/>
        /// </summary>
        public int StatusFlag { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 帐号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 数据较验
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (UserRoleId <= 0)
            {
                return false;
            }
            if (string.IsNullOrEmpty(NickName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(Account))
            {
                return false;
            }
            if (UserId == 0 && string.IsNullOrEmpty(Password))
            {
                return false;
            }
            return true;
        }
    }
}
